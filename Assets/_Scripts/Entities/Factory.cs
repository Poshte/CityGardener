using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	private WealthManager wealthManager;
	private PollutionManager pollutionManager;

	private const int wealthAmount = 35;
	private const float wealthRate = 10f;
	private float wealthTimer;

	private const int pollutionAmount = 1;
	private const float pollutionRate = 10f;
	private float pollutionTimer;

	//TODO
	//maybe should set these once from CityManager
	//for performance
	private const int wealthFactor = 2;

	public List<Citizen> Workers = new();
	public bool IsOperational { get; private set; }
	public bool MaxedOut;

	[SerializeField] private SpriteRenderer nonOperationalSprite;

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
			return;

		wealthTimer += Time.deltaTime;
		if (wealthTimer > wealthRate)
			ProduceWealth();

		pollutionTimer += Time.deltaTime;
		if (pollutionTimer > pollutionRate)
			IncreasePollution();
	}

	private void ProduceWealth()
	{
		var wealth = MaxedOut ? wealthAmount * wealthFactor : wealthAmount;
		wealthManager.AddWealth(wealth);
		wealthTimer = 0f;
	}

	private void IncreasePollution()
	{
		pollutionManager.IncreasePollution(pollutionAmount);
		pollutionTimer = 0f;
	}

	public void BeginOperation()
	{
		IsOperational = true;
		nonOperationalSprite.enabled = false;
	}
}
