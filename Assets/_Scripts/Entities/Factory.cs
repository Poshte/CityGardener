using UnityEngine;

public class Factory : MonoBehaviour
{
	private WealthManager wealthManager;
	private PollutionManager pollutionManager;

	[SerializeField] private float wealthAmount = 100f;
	[SerializeField] private int pollutionAmount = 10;

	private float productionRate = 1f;
	private float productionTimer;

	private void Awake()
	{
		var gameController = GameObject.FindGameObjectWithTag(Constants.Tags.GameController);
		pollutionManager = gameController.GetComponent<PollutionManager>();
		wealthManager = gameController.GetComponent<WealthManager>();
	}

	private void Update()
	{
		productionTimer += Time.deltaTime;

		if (productionTimer > productionRate)
		{
			//emit pollution
			pollutionManager.IncreasePollution(pollutionAmount);

			//increase wealth
			wealthManager.AddWealth(wealthAmount);

			productionTimer = 0f;
		}
	}
}
