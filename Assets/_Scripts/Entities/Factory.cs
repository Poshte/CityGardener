using UnityEngine;

public class Factory : MonoBehaviour
{
	[SerializeField]
	private float wealthAmount = 100f;
	[SerializeField]
	private int pollutionAmount = 1;

	private float productionRate = 100f;
	private float productionTimer;

	private void Update()
	{
		productionTimer += Time.deltaTime;

		if (productionTimer > productionRate)
		{
			//add wealth
			GameManager.AddWealth(wealthAmount);

			//emit CO2
			GameManager.EmitCO2(pollutionAmount);
		}
	}
}
