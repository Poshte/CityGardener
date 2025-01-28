using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField]
	public GrowthState GrowthState;
	public bool NeedsWater = true;

	public void Grow()
	{
		//grow the tree


		//TODO
		//set a timer to determine when it needs water again
		GrowthState += 1;
		NeedsWater = false;
	}
}