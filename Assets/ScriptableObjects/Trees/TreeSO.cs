using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TreeSO : ScriptableObject
{
	public List<TreeEntity> TreePrefabs = new();
}
