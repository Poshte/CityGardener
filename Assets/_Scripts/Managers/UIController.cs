using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button btnBucket;
	[SerializeField] private Button btnHouse;
	[SerializeField] private Button btnFactory;
	[SerializeField] private Button btnTree;
	[SerializeField] private GameObject TreeTypesUI;

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

	public void OnFirTreeClicked()
	{
		PlantTree(TreeTypes.Fir);
	}

	public void OnOakTreeClicked()
	{
		PlantTree(TreeTypes.Oak);
	}

	public void OnBirchTreeClicked()
	{
		PlantTree(TreeTypes.Birch);
	}

	private void PlantTree(TreeTypes treeType)
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

	public void OnNextLevelClicked()
	{
		SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void ClearUp()
	{
		btnTree.image.color = btnHouse.image.color = btnFactory.image.color = Color.yellow;
		TreeTypesUI.SetActive(false);
		buildingManager.SetActiveBuildingType(null);
	}
}