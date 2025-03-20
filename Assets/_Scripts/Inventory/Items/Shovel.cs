using System.Collections;
using UnityEngine;

public class Shovel : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.Shovel;
	public override TreeType SeedType => TreeType.None;
	public override bool Stackable => false;

	private const float duration = 3f;

	public override void PerformAction()
	{
		tillSoilCoroutine = StartCoroutine(TillSoil());
	}

	private Coroutine tillSoilCoroutine;
	[SerializeField] private Transform gardenPrefab;
	private Player player;

	public override void Awake()
	{
		base.Awake();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	private void Update()
	{
		//stop tilling if player moved
		if (tillSoilCoroutine != null)
		{
			if (player.IsWalking)
			{
				StopCoroutine(tillSoilCoroutine);
				tillSoilCoroutine = null;
			}
		}
	}

	public IEnumerator TillSoil()
	{
		var elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			//TODO
			//play animation
			Debug.Log(elapsedTime);

			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}

		//spawn garden if coroutine finishes successfuly
		Instantiate(gardenPrefab, player.transform.position, Quaternion.identity);
	}
}