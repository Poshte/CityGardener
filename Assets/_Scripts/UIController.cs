using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private Button btnBucket;

	[SerializeField]
	private Button btnTree;

	[SerializeField]
	private Button btnHouse;

	[SerializeField]
	private Button btnFactory;

	public void OnBucketClicked()
	{
		if (GameManager.NearWater)
		{
			GameManager.BucketFilledWithWater = true;
			btnBucket.image.color = Color.blue;
		}
		else if (GameManager.NearTree)
		{
			btnBucket.image.color = Color.yellow;
			GameEvents.Instance.WateringTree();
		}
	}

	public void OnTreeClicked()
	{
		if (GameManager.NearGarden)
		{
			//TODO
			//should implement a feature to plant different trees
			//btnTree.image.color = Color.red;
			GameEvents.Instance.PlantingTree();
		}
	}

	public void OnHouseClicked()
	{
		Debug.Log("House Clicked");
	}

	public void OnFactoryClicked()
	{
		Debug.Log("Factory Clicked");
	}
}
