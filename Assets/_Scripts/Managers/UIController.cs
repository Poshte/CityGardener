using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button btnBucket;
	[SerializeField] private RectTransform bucketHandle;
	private const int WaterLevelFactor = 27;

	[SerializeField] private Button btnHouse;
	[SerializeField] private Button btnFactory;
	[SerializeField] private Button btnTree;

	[SerializeField] private GameObject TreeTypesUI;
	
	[SerializeField] private BuildingTypeSO houseSO;
	[SerializeField] private BuildingTypeSO factorySO;

	[SerializeField] private BuildingManager buildingManager;

	private Player player;

	private Color selectedColor;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	private void OnEnable()
	{
		GameEvents.Instance.OnTreeWatered += OnTreeWatered;
	}

	private void Start()
	{
		var temp = Color.yellow;
		temp.a = 0.4f;
		selectedColor = temp;
	}

	public void OnBucketClicked()
	{
		ClearUp();

		if (GameManager.NearWater)
		{
			GameManager.WaterCanLevel = 5;
			var waterLevel = GameManager.WaterCanLevel * WaterLevelFactor;
			UpdateBucketHandleSize(waterLevel);
		}
		else if (GameManager.ActiveTrees.Any())
		{
			FindNearest(GameManager.ActiveTrees).WaterTree();
		}
		else if (GameManager.WaterCanLevel == 0)
		{
			StartCoroutine(ColorUtility.ChangeColor(btnBucket.image, selectedColor, 0.5f));
		}

		buildingManager.SetActiveBuildingType(null);
	}

	public void OnTreeClicked()
	{
		ClearUp();
		btnTree.image.color = selectedColor;
		TreeTypesUI.SetActive(true);
		buildingManager.SetActiveBuildingType(null);
	}

	public void OnFirTreeClicked()
	{
		PlantTree(TreeType.Fir);
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

		FindNearest(GameManager.NearbyGardens).PlantTree(treeType);
		btnTree.image.color = Color.yellow;
		TreeTypesUI.SetActive(false);
	}

	public void OnHouseClicked()
	{
		ClearUp();
		btnHouse.image.color = selectedColor;
		buildingManager.SetActiveBuildingType(houseSO);
	}

	public void OnFactoryClicked()
	{
		ClearUp();
		btnFactory.image.color = selectedColor;
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

	private T FindNearest<T>(List<T> items) where T : MonoBehaviour
	{
		float nearestDistance = Mathf.Infinity;
		T nearestObj = default;

		foreach (var item in items)
		{
			var distance = Vector2.Distance(player.transform.position, item.transform.position);

			if (distance < nearestDistance)
			{
				nearestDistance = distance;
				nearestObj = item;
			}
		}

		return nearestObj;
	}

	private void OnTreeWatered()
	{
		var waterLevel = GameManager.WaterCanLevel * WaterLevelFactor;
		UpdateBucketHandleSize(waterLevel);
	}

	private void UpdateBucketHandleSize(int waterLevel)
	{
		var temp = bucketHandle.sizeDelta;
		temp.x = waterLevel;
		bucketHandle.sizeDelta = temp;
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnTreeWatered -= OnTreeWatered;
	}
}