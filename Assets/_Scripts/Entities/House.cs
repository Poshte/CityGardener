using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
	public List<Citizen> Inhabitants = new();

	private void Start()
	{
		Inhabitants.Add(new Citizen(this));
		Inhabitants.Add(new Citizen(this));
		Inhabitants.Add(new Citizen(this));

		GameEvents.Instance.HouseCreated(this);
	}
}
