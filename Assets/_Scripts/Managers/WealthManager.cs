using TMPro;
using UnityEngine;

public class WealthManager : MonoBehaviour
{
	private int wealth = 450;
	[SerializeField] private TextMeshProUGUI wealthAmount;

	private void Start()
	{
		UpdateWealthUI();
	}

	public void AddWealth(int amount)
	{
		wealth += amount;
		UpdateWealthUI();
	}

	public bool CanAfford(int amount)
	{
		if (wealth < amount)
		{
			StartCoroutine(ColorUtility.Blink(wealthAmount, Color.red, 0.75f, 5));
			return false;
		}

		return true;
	}

	public bool SpendWealth(int amount)
	{
		if (!CanAfford(amount))
			return false;

		wealth -= amount;
		StartCoroutine(ColorUtility.RevertColor(wealthAmount, Color.red, 0.5f));
		UpdateWealthUI();
		return true;
	}

	private void UpdateWealthUI()
	{
		wealthAmount.text = wealth.ToString();
	}
}
