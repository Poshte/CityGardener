using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Button btnWateringCan;

	[SerializeField] private Button btnTree;
	[SerializeField] private Button btnHouse;
	[SerializeField] private Button btnFactory;
	[SerializeField] private Button btnPipe;

	[SerializeField] private GameObject TreeTypesUI;

	[SerializeField] private BuildingTypeSO houseSO;
	[SerializeField] private BuildingTypeSO factorySO;
	[SerializeField] private PipeSO dripPipeSO;

	private BuildingManager buildingManager;
	private PipeBuilder pipeBuilder;
	private Player player;

	private Color selectedColor;

	private void Awake()
	{
		buildingManager = GameObject.FindGameObjectWithTag(Constants.Tags.BuildingManager).GetComponent<BuildingManager>();
		pipeBuilder = GameObject.FindGameObjectWithTag(Constants.Tags.PipeBuilder).GetComponent<PipeBuilder>();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	private void Start()
	{
		var temp = Color.yellow;
		temp.a = 0.4f;
		selectedColor = temp;
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
		btnTree.image.color = btnHouse.image.color = btnFactory.image.color = btnPipe.image.color = Color.yellow;
		TreeTypesUI.SetActive(false);
		buildingManager.SetActiveBuildingType(null);
		pipeBuilder.SetActivePipe(null);
	}
}