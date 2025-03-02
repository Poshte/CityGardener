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

	public bool SpendWealth(int amount)
	{
		if (wealth < amount)
		{
			Debug.Log("Not enough money");
			return false;
		}

		wealth -= amount;
		UpdateWealthUI();
		return true;
	}

	private void UpdateWealthUI()
	{
		wealthAmount.text = wealth.ToString();
	}
}
