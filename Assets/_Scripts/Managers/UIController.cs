using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private GameObject actionBar;

	public Button BtnHouse { get => btnHouse; }
	[SerializeField] private Button btnHouse;
	public Button BtnFactory { get => btnFactory; }
	[SerializeField] private Button btnFactory;
	public Button BtnPipe { get => btnPipe; }
	[SerializeField] private Button btnPipe;

	[SerializeField] private BuildingTypeSO houseSO;
	[SerializeField] private BuildingTypeSO factorySO;
	[SerializeField] private PipeSO dripPipeSO;

	private BuildingManager buildingManager;
	private PipeBuilder pipeBuilder;

	private void Awake()
	{
		buildingManager = GameObject.FindGameObjectWithTag(Constants.Tags.BuildingManager).GetComponent<BuildingManager>();
		pipeBuilder = GameObject.FindGameObjectWithTag(Constants.Tags.PipeBuilder).GetComponent<PipeBuilder>();
	}

	private void Start()
	{
		GameEvents.Instance.OnStoreOpened += OnStoreOpened;
		GameEvents.Instance.OnStoreClosed += OnStoreClosed;
	}

	public void OnHouseClicked()
	{
		ClearUp();
		buildingManager.SetActiveBuildingType(houseSO);
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
	}
}