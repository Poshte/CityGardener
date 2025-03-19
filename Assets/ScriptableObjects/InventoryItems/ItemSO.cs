using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
	public Sprite Sprite;
	public InventoryItemType Type;
	public TreeType TreeType = TreeType.None;
	public bool Stackable;
}