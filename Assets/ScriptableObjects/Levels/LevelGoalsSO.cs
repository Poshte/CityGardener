using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelGoalsSO : ScriptableObject
{
	public List<TreeType> TreesToPlant = new();
	public int PollutionGoal;
}
