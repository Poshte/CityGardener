using TMPro;
using UnityEngine;

public class PollutionManager : MonoBehaviour
{
	private int pollution = 195;
	private const int maxPollution = 200;

	[SerializeField] private TextMeshProUGUI pollutionAmount;

	private const float passivePollutionRate = 5f;
	private float passivePollutionTimer;

	private void Start()
	{
		UpdatePollutionUI();
	}

	private void Update()
	{
		passivePollutionTimer += Time.deltaTime;
		if (passivePollutionTimer > passivePollutionRate)
		{
			IncreasePollution(1);
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
				color = new Color(0f, 1f, 0f);
				break;

			case int i when i >= 50 && i < 100:
				color = new Color(1f, 0.95f, 0f);
				break;

			case int i when i >= 100 && i < 150:
				color = new Color(1f, 0.7f, 0f);
				break;

			case > 150:
				color = new Color(1f, 0.09f, 0f);
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
			LoseLevel();
	}

	private void LoseLevel()
	{
		Debug.Log("YOU LOST...");
		//TODO
		//show lose panel in UI
	}
}
