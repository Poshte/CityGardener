using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
	private BuildingTypeSO activeBuildingType;
	private PlayerInput input;
	[SerializeField] private WealthManager wealthManager;

	private void Awake()
	{
		input = new PlayerInput();
	}

	private void Update()
	{
		if (activeBuildingType == null)
			return;

		if (input.PlayerInputs.MouseLeftClick.WasPressedThisFrame() && !EventSystem.current.IsPointerOverGameObject())
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
			return false;

		return true;
	}

	private void OnEnable()
	{
		input.PlayerInputs.Enable();
	}

	private void OnDisable()
	{
		input.PlayerInputs.Disable();
	}
}
