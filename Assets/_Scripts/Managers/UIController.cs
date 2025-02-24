using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button btnBucket;
	[SerializeField] private Button btnTree;
	[SerializeField] private Button btnHouse;
	[SerializeField] private Button btnFactory;

	[SerializeField] private GameObject TreeTypesUI;
	[SerializeField] private Button btnPineTree;
	[SerializeField] private Button btnOakTree;
	[SerializeField] private Button btnBirchTree;

	[SerializeField] private BuildingTypeSO houseSO;
	[SerializeField] private BuildingTypeSO factorySO;

	[SerializeField] private BuildingManager buildingManager;

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

		buildingManager.SetActiveBuildingType(null);
	}

	public void OnTreeClicked()
	{
		btnTree.image.color = Color.red;
		TreeTypesUI.SetActive(true);
		buildingManager.SetActiveBuildingType(null);
	}

	public void OnPineTreeClicked()
	{
		PlantTree(TreeType.Pine);
	}

	public void OnOakTreeClicked()
	{
		PlantTree(TreeType.Oak);
	}

	public void OnBirchTreeClicked()
	{
		PlantTree(TreeType.Birch);
	}

	private void PlantTree(TreeType treeType)
	{
		if (!GameManager.NearbyGardens.Any())
		{
			ClearUp();
			return;
		}

		GameManager.NearbyGardens.FirstOrDefault().PlantTree(treeType);
		btnTree.image.color = Color.yellow;
		TreeTypesUI.SetActive(false);
	}

	public void OnHouseClicked()
	{
		buildingManager.SetActiveBuildingType(houseSO);
	}

	public void OnFactoryClicked()
	{
		buildingManager.SetActiveBuildingType(factorySO);
	}

	public void ClearUp()
	{
		btnTree.image.color = btnHouse.image.color = btnFactory.image.color = Color.yellow;
		TreeTypesUI.SetActive(false);
		buildingManager.SetActiveBuildingType(null);
	}
}