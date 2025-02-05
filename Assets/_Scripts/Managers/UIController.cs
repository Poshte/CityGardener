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
	}

	public void OnTreeClicked()
	{
		if (GameManager.ActiveGarden != null)
		{
			btnTree.image.color = Color.red;

			TreeTypesUI.SetActive(true);
		}
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
		GameManager.ActiveGarden.PlantTree(treeType);
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

	private void ClearUp()
	{
		//TODO
		//When mouse is clicked anywhere other than UI
		//When player moves away from garden
	}
}