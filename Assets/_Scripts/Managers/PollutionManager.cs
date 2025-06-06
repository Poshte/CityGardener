using TMPro;
using UnityEngine;

public class PollutionManager : MonoBehaviour
{
	private int pollution = 130;
	private const int maxPollution = 200;

	[SerializeField] private TextMeshProUGUI pollutionAmount;

	private int passivePollutionFactor = 1;
	private const float passivePollutionRate = 20f;
	private float passivePollutionTimer;

	private Color orange = new(1f, 0.7f, 0f);

	private void Start()
	{
		UpdatePollutionUI();
	}

	private void Update()
	{
		passivePollutionTimer += Time.deltaTime;
		if (passivePollutionTimer > passivePollutionRate)
		{
			IncreasePollution(passivePollutionFactor);
			passivePollutionTimer = 0f;
		}
	}

	public void IncreasePollution(int amount)
	{
		pollution += amount;
		UpdatePollutionUI();
		CheckLoseCondition();
	}

	public void ReducePollution(int amount)
	{
		if (pollution == 0)
			return;

		pollution -= amount;

		if (pollution < 0)
			pollution = 0;

		UpdatePollutionUI();
	}

	private void UpdatePollutionUI()
	{
		var color = Color.white;

		switch (pollution)
		{
			case < 50:
				color = Color.green;
				passivePollutionFactor = 1;
				break;

			case int i when i >= 50 && i < 100:
				color = Color.yellow;
				passivePollutionFactor = 2;
				break;

			case int i when i >= 100 && i < 150:
				color = orange;
				passivePollutionFactor = 3;
				break;

			case > 150:
				color = Color.red;
				passivePollutionFactor = 4;
				break;

			default:
				break;
		}

		pollutionAmount.color = color;
		pollutionAmount.text = pollution.ToString();
	}

	private void CheckLoseCondition()
	{
		if (pollution >= maxPollution)
		{
			GameEvents.Instance.LoseLevel();
		}
	}

	public int GetCurrentPollutionIndex()
	{
		return pollution;
	}
}
