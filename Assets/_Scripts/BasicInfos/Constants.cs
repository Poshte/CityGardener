using UnityEngine;

public static class Constants
{
	public static class Tags
	{
		public const string Player = "Player";
		public const string PollutionManager = "PollutionManager";
		public const string WealthManager = "WealthManager";
		public const string BuildingManager = "BuildingManager";
		public const string WaterResource = "WaterResource";
		public const string Pipe = "Pipe";
		public const string PipeBuilder = "PipeBuilder";
	}

	public static class LayerMasks
	{
		public const string Interactable = "Interactable";
	}

	public static class Colors
	{
		public static Color ValidBuildingSilhouette = new(0.2f, 0.9f, 0.4f, 0.2f);
		public static Color InvalidBuildingSilhouette = new(1f, 0f, 0f, 0.2f);
	}
}