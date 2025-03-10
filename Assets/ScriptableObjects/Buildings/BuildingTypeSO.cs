using UnityEngine;

[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject
{
	public Transform Prefab;
	public BuildingType Type;
	public int Cost;
}