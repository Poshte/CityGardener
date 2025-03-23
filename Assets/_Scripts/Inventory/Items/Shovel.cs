using System.Linq;
using UnityEngine;

public class Shovel : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.Shovel;
	public override TreeType SeedType => TreeType.None;
	public override bool Stackable => false;

	private const float duration = 3f;
	private const float Garden_Search_Radius = 0.5f;

	private bool tilling;
	private Vector2 tillingPos;
	private float elapsedTime = 0f;

	[SerializeField] private Transform gardenPrefab;
	private Player player;

	public override void Awake()
	{
		base.Awake();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	private void Update()
	{
		if (!tilling)
			return;

		TillSoil();

		if (player.IsWalking)
			StopTilling();
	}

	public override void PerformAction(Vector2? targetPos)
	{
		if (targetPos == null)
			return;

		tillingPos = (Vector2)targetPos;

		var hits = Physics2D.CircleCastAll(tillingPos, Garden_Search_Radius, Vector2.zero);
		var hitGarden = hits.FirstOrDefault(h => h.collider != null && h.collider.CompareTag(Constants.Tags.Garden));

		if (hitGarden)
		{
			Debug.Log("The soil here is already tilled");
			return;
		}

		tilling = true;
	}

	private void TillSoil()
	{
		//TODO
		//play animation

		elapsedTime += Time.unscaledDeltaTime;
		if (elapsedTime > duration)
		{
			Instantiate(gardenPrefab, tillingPos, Quaternion.identity);
			StopTilling();
		}
	}

	private void StopTilling()
	{
		tilling = false;
		elapsedTime = 0f;
		tillingPos = Vector2.zero;
	}
}