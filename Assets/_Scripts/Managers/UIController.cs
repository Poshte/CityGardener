using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
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

	private Color selectedColor;


	private void Awake()
	{
		buildingManager = GameObject.FindGameObjectWithTag(Constants.Tags.BuildingManager).GetComponent<BuildingManager>();
		pipeBuilder = GameObject.FindGameObjectWithTag(Constants.Tags.PipeBuilder).GetComponent<PipeBuilder>();
	}

	private void Start()
	{
		var temp = Color.yellow;
		//temp.a = 0.4f;
		selectedColor = temp;
	}

	public void OnHouseClicked()
	{
		ClearUp();
		BtnHouse.image.color = selectedColor;
		buildingManager.SetActiveBuildingType(houseSO);
	}

	public void OnFactoryClicked()
	{
		ClearUp();
		btnFactory.image.color = selectedColor;
		buildingManager.SetActiveBuildingType(factorySO);
	}

	public void OnPipeClicked()
	{
		ClearUp();
		btnPipe.image.color = selectedColor;
		pipeBuilder.SetActivePipe(dripPipeSO);
	}

	public void OnNextLevelClicked()
	{
		SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void ClearUp()
	{
		BtnHouse.image.color = btnFactory.image.color = btnPipe.image.color = Color.yellow;
		buildingManager.SetActiveBuildingType(null);
		pipeBuilder.SetActivePipe(null);
	}
}