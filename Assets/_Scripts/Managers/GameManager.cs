using System.Collections.Generic;

public static class GameManager
{
	public static bool NearWater;
	public static bool BucketFilledWithWater;

	public static List<Garden> NearbyGardens = new();
	public static TreeEntity ActiveTree;
}
