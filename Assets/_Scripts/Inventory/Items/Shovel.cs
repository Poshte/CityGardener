using UnityEngine;

public class Shovel : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.Shovel;
	public override TreeType SeedType => TreeType.None;
	public override bool Stackable => false;
	private const float duration = 3f;

	private bool tilling;
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

	private void TillSoil()
	{
		//TODO
		//play animation

		elapsedTime += Time.unscaledDeltaTime;
		if (elapsedTime > duration)
		{
			Instantiate(gardenPrefab, player.transform.position, Quaternion.identity);
			StopTilling();
		}
	}

	private void StopTilling()
	{
		tilling = false;
		elapsedTime = 0f;
	}

	public override void PerformAction()
	{
		tilling = true;
	}
}