using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
	private static GameEvents _instance;
	public static GameEvents Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	public event Action<House> OnHouseCreated;
	public void HouseCreated(House house) => OnHouseCreated?.Invoke(house);

	public event Action<Factory> OnFactoryCreated;
	public void FactoryCreated(Factory factory) => OnFactoryCreated?.Invoke(factory);
}
