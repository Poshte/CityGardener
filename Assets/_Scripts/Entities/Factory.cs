using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	private WealthManager wealthManager;
	private PollutionManager pollutionManager;

	[SerializeField] private float wealthAmount = 100f;
	[SerializeField] private int pollutionAmount = 10;

	private const float productionRate = 3f;
	private float productionTimer;

    public List<Citizen> Workers = new();
	public bool IsOperational;
	public bool MaxedOut;

	private void Awake()
	{
		pollutionManager = GameObject.FindGameObjectWithTag(Constants.Tags.PollutionManager).GetComponent<PollutionManager>();
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	private void Start()
	{
		GameEvents.Instance.FactoryCreated(this);
	}

	private void Update()
	{
		if (!IsOperational)
		{
			//TODO
			//show the factory is not operational in UI
			return;
		}

		productionTimer += Time.deltaTime;
		if (productionTimer > productionRate)
			Produce();
	}

	private void Produce()
	{
		pollutionManager.IncreasePollution(pollutionAmount);
		wealthManager.AddWealth(wealthAmount);
		productionTimer = 0f;
	}
}
