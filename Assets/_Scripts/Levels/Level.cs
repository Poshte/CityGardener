using UnityEngine;

public class Level : MonoBehaviour
{
	public GameScene GameScene => _gameScene;
	[SerializeField] private GameScene _gameScene;
	public InventoryItemType[] InventoryItems => _inventoryItems;
	[SerializeField] private InventoryItemType[] _inventoryItems;
	public ActionBarItemType[] ActionBarItems => _actionBarItems;
	[SerializeField] private ActionBarItemType[] _actionBarItems;
	public bool Unlocked { get => _unlocked; set => _unlocked = value; }
	[SerializeField] private bool _unlocked;
}