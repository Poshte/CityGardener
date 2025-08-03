using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class UIController : MonoBehaviour
{
	[SerializeField] private GameObject actionBar;
	[SerializeField] private ActionBarItem[] actionBarItemPrefabs;

	[SerializeField] private BuildingTypeSO[] houseSOList;
	[SerializeField] private BuildingTypeSO factorySO;
	[SerializeField] private PipeSO dripPipeSO;

	private BuildingManager buildingManager;
	private PipeBuilder pipeBuilder;
	private InventoryManager inventoryManager;

	private void Awake()
	{
		buildingManager = GameObject.FindGameObjectWithTag(Constants.Tags.BuildingManager).GetComponent<BuildingManager>();
		pipeBuilder = GameObject.FindGameObjectWithTag(Constants.Tags.PipeBuilder).GetComponent<PipeBuilder>();
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
	}

	private void OnEnable()
	{
		GameEvents.Instance.OnStoreOpened += OnStoreOpened;
		GameEvents.Instance.OnStoreClosed += OnStoreClosed;
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnStoreOpened -= OnStoreOpened;
		GameEvents.Instance.OnStoreClosed -= OnStoreClosed;
	}

	public void AddActionBarItem(ActionBarItemType itemType)
	{
		var item = actionBarItemPrefabs.FirstOrDefault(i => i.Type == itemType);
		AddActionBarItem(item);
	}

	public void AddActionBarItem(ActionBarItem item)
	{
		var actionBarItem = Instantiate(item, actionBar.transform);
		actionBarItem.UIController = this;
	}

	public void OnHouseClicked()
	{
		ClearUp();
		buildingManager.SetActiveBuildingType(houseSOList[Random.Range(0, houseSOList.Length)]);
	}

	public void OnFactoryClicked()
	{
		ClearUp();
		buildingManager.SetActiveBuildingType(factorySO);
	}

	public void OnPipeClicked()
	{
		ClearUp();
		pipeBuilder.SetActivePipe(dripPipeSO);
	}

	public void OnNextLevelClicked()
	{
		//fire BeforeSceneDestroyed event
		GameEvents.Instance.BeforeSceneDestroyed();
		SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void OnStoreOpened()
	{
		actionBar.SetActive(false);
	}

	private void OnStoreClosed()
	{
		actionBar.SetActive(true);
	}

	public void ClearUp()
	{
		buildingManager.SetActiveBuildingType(null);
		pipeBuilder.SetActivePipe(null);
		inventoryManager.ClearSelectedItem();
	}
}