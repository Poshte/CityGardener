using TMPro;
using UnityEngine;

public class WealthManager : MonoBehaviour
{
	private float wealth;
	[SerializeField] private TextMeshProUGUI wealthText;

	public void AddWealth(float amount)
	{
		wealth += amount;
		UpdateWealthUI();
	}

	public void SpendWealth(float amount)
	{
		wealth -= amount;
		UpdateWealthUI();
	}

	private void UpdateWealthUI()
	{
		wealthText.text = wealth.ToString();
	}
}
