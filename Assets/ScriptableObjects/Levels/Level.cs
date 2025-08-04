using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Level : ScriptableObject
{
	public GameScene GameScene;
	public InventoryItemType[] InventoryItems;
	public ActionBarItemType[] ActionBarItems;
	public bool Unlocked;
	public List<TreeType> TreesToPlant = new();
	public int PollutionGoal;
	public int startingPollution;
}