using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
	public List<TreeType> TreesToPlant = new();
	public int PollutionGoal;
}
