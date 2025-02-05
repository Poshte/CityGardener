using TMPro;
using UnityEngine;

public class PollutionManager : MonoBehaviour
{
	private int pollution = 200;

	[SerializeField] private TextMeshProUGUI pollutionAmount;

	private void Start()
	{
		UpdatePollutionUI();
	}

	public void IncreasePollution(int amount)
	{
		pollution += amount;
		UpdatePollutionUI();
	}

	public void DecreasePollution(int amount)
	{
		pollution -= amount;
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
}
