using System.Collections.Generic;

public static class GameManager
{
	public static bool NearWater;
	public static int WaterCanLevel;

	public static List<Garden> NearbyGardens = new();
	public static List<TreeEntity> ActiveTrees = new();
}
