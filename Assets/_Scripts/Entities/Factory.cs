using System;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	private WealthManager wealthManager;
	private PollutionManager pollutionManager;

	[SerializeField] private float wealthAmount = 100f;
	[SerializeField] private int pollutionAmount = 10;

	private float productionRate = 1f;
	private float productionTimer;

    public List<Citizen> Workers = new();
	public bool IsOperational;
	public bool MaxedOut;

	private void Awake()
	{
		var gameController = GameObject.FindGameObjectWithTag(Constants.Tags.GameController);
		pollutionManager = gameController.GetComponent<PollutionManager>();
		wealthManager = gameController.GetComponent<WealthManager>();
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
		{
			//emit pollution
			pollutionManager.IncreasePollution(pollutionAmount);

			//increase wealth
			wealthManager.AddWealth(wealthAmount);

			productionTimer = 0f;
		}
	}
}
