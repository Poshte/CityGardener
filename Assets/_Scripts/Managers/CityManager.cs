using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CityManager : MonoBehaviour
{
	private readonly List<Factory> factories = new();
	private readonly List<Citizen> citizens = new();

	private const int minFactoryOperators = 2;
	private const int maxFactoryOperators = 5;

	[SerializeField] private TextMeshProUGUI employedText;
	[SerializeField] private TextMeshProUGUI populationText;

	private void Start()
	{
		GameEvents.Instance.OnHouseCreated += HouseCreated;
		GameEvents.Instance.OnFactoryCreated += FactoryCreated;
	}

	private void HouseCreated(House house)
	{
		citizens.AddRange(house.Inhabitants);
		AssignUnemployedCitizensToFactories();
		UpdatePopulationUI();
	}

	private void FactoryCreated(Factory factory)
	{
		factories.Add(factory);
		AssignUnemployedCitizensToFactories();
		UpdatePopulationUI();
	}

	private void AssignUnemployedCitizensToFactories()
	{
		var unemployed = citizens.Where(c => !c.Employed);

		foreach (var citizen in unemployed)
		{
			var inoperativeFactories = factories.Where(f => !f.MaxedOut).OrderBy(f => f.IsOperational);

			foreach (var factory in inoperativeFactories)
			{
				if (!factory.MaxedOut)
				{
					factory.Workers.Add(citizen);
					citizen.Employed = true;

					if (factory.Workers.Count >= minFactoryOperators)
						factory.IsOperational = true;

					if (factory.Workers.Count >= maxFactoryOperators)
						factory.MaxedOut = true;

					break;
				}
			}
		}
	}

	private void UpdatePopulationUI()
	{
		employedText.text = citizens.Count(c => c.Employed).ToString();
		populationText.text = citizens.Count.ToString();
	}

	private void OnDestroy()
	{
		GameEvents.Instance.OnHouseCreated -= HouseCreated;
		GameEvents.Instance.OnFactoryCreated -= FactoryCreated;
	}
}
