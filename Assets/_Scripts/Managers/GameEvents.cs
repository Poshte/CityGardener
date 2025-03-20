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

	public event Action OnBeforeSceneDestroyed;
	public void BeforeSceneDestroyed() => OnBeforeSceneDestroyed?.Invoke();

	public event Action<House> OnHouseCreated;
	public void HouseCreated(House house) => OnHouseCreated?.Invoke(house);

	public event Action<Factory> OnFactoryCreated;
	public void FactoryCreated(Factory factory) => OnFactoryCreated?.Invoke(factory);

	public event Action<TreeType> OnMatureTreePlanted;
	public void MatureTreePlanted(TreeType tree) => OnMatureTreePlanted?.Invoke(tree);

	public event Action OnTreeWatered;
	public void TreeWatered() => OnTreeWatered?.Invoke();

	public event Action OnWinningLevel;
	public void WinLevel() => OnWinningLevel?.Invoke();

	public event Action OnLosingLevel;
	public void LoseLevel() => OnLosingLevel?.Invoke();

	public event Action<InventorySlot> OnItemSelected;
	public void ItemSelected(InventorySlot slot) => OnItemSelected?.Invoke(slot);
}