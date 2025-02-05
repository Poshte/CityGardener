using System;

public class GameEvents
{
	private static readonly object _lock = new();
	private static GameEvents _instance;
	public static GameEvents Instance
	{
		get
		{
			if (_instance == null)
			{
				lock (_lock)
				{
					_instance ??= new();
				}
			}

			return _instance;
		}
	}

	private GameEvents()
	{
	}

	public event Action<House> OnHouseCreated;
	public void HouseCreated(House house) => OnHouseCreated?.Invoke(house);

	public event Action<Factory> OnFactoryCreated;
	public void FactoryCreated(Factory factory) => OnFactoryCreated?.Invoke(factory);
}