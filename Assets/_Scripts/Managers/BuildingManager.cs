using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
	private BuildingTypeSO activeBuildingType;
	private PlayerInput input;
	private WealthManager wealthManager;
	[SerializeField] private UIController uiController;

	private void Awake()
	{
		input = new PlayerInput();
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	private void Update()
	{
		if (activeBuildingType == null)
			return;

		if (input.Construction.MouseLeftClick.WasPerformedThisFrame() && !EventSystem.current.IsPointerOverGameObject())
		{
			var mousePos = GetMouseWorldPosition();

			if (!CanSpawnBuilding(mousePos))
				return;

			if (!PayBuildingCost())
				return;

			ConstructBuilding(mousePos);
		}
	}

	private bool PayBuildingCost()
	{
		return wealthManager.SpendWealth(activeBuildingType.Cost);
	}

	private void ConstructBuilding(Vector2 spawnPos)
	{
		Instantiate(activeBuildingType.Prefab, spawnPos, Quaternion.identity);
		activeBuildingType = null;
		uiController.ClearUp();
	}

	public void SetActiveBuildingType(BuildingTypeSO buildingType)
	{
		activeBuildingType = buildingType;
	}

	private Vector2 GetMouseWorldPosition()
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
		worldPos.z = 0f;
		return worldPos;
	}

	private bool CanSpawnBuilding(Vector2 pos)
	{
		var buildingCollider = activeBuildingType.Prefab.GetComponent<BoxCollider2D>();

		if (Physics2D.OverlapBox(pos + buildingCollider.offset, buildingCollider.size, 0))
		{
			Debug.Log("Can't place there.");
			return false;
		}

		return true;
	}

	private void OnEnable()
	{
		input.Construction.Enable();
	}

	private void OnDisable()
	{
		input.Construction.Disable();
	}
}
