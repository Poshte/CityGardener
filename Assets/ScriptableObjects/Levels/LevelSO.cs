using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
	public List<TreeTypes> TreesToPlant = new();
	public int PollutionGoal;
}
