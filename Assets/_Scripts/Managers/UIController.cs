using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button btnBucket;
	[SerializeField] private Button btnTree;
	[SerializeField] private Button btnHouse;
	[SerializeField] private Button btnFactory;

	[SerializeField] private BuildingTypeSO houseSO;
	[SerializeField] private BuildingTypeSO factorySO;

	private BuildingManager buildingManager;

	private void Awake()
	{
		buildingManager = GetComponent<BuildingManager>();
	}

	public void OnBucketClicked()
	{
		if (GameManager.NearWater)
		{
			GameManager.BucketFilledWithWater = true;
			btnBucket.image.color = Color.blue;
		}
		else if (GameManager.ActiveTree != null)
		{
			btnBucket.image.color = Color.yellow;
			GameManager.ActiveTree.WaterTree();
		}
	}

	public void OnTreeClicked()
	{
		if (GameManager.ActiveGarden != null)
		{
			//TODO
			//should implement a feature to plant different trees
			//btnTree.image.color = Color.red;
			GameManager.ActiveGarden.PlantTree();
		}
	}

	public void OnHouseClicked()
	{
		buildingManager.SetActiveBuildingType(houseSO);
	}

	public void OnFactoryClicked()
	{
		buildingManager.SetActiveBuildingType(factorySO);
	}
}
