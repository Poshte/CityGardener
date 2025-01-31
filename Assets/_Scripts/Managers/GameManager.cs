using UnityEngine;

public static class GameManager
{
	private static float wealth;
	private static int co2;

	public static bool NearWater;
	public static bool BucketFilledWithWater;
	
	public static Garden ActiveGarden;
	public static TreeEntity ActiveTree;

	public static void AddWealth(float amount)
	{
		wealth += amount;
		Debug.Log("Wealth: " + wealth);
	}

	public static void SpendWealth(float amount)
	{
		wealth -= amount;
		Debug.Log("Wealth: " + wealth);
	}

	public static void EmitCO2(int amount)
	{
		co2 += amount;
		Debug.Log("CO2:" + co2);
	}

	public static void DecreaseCO2(int amount)
	{
		co2 -= amount;
		Debug.Log("CO2:" + co2);
	}
}
