using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	private WealthManager wealthManager;
	private PollutionManager pollutionManager;

	private int wealthProduction = 35;
	private const int productionFactor = 2;
	private const float productionRate = 10f;
	private float productionTimer;

	private const int pollutionAmount = 1;
	private const float pollutionRate = 10f;
	private float pollutionTimer;

	[SerializeField] private SpriteRenderer nonOperationalSprite;

	public bool IsOperational { get; private set; }
	public bool MaxedOut { get; private set; }
	public List<Citizen> Workers { get; private set; } = new();

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

		productionTimer += Time.deltaTime;
		if (productionTimer > productionRate)
			ProduceWealth();

		pollutionTimer += Time.deltaTime;
		if (pollutionTimer > pollutionRate)
			IncreasePollution();
	}

	private void ProduceWealth()
	{
		wealthManager.AddWealth(wealthProduction);
		productionTimer = 0f;
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

	public void MaxOut()
	{
		MaxedOut = true;
		wealthProduction *= productionFactor;
	}
}
