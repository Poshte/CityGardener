using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
	private BuildingTypeSO activeBuildingType;
	private PlayerInput input;
	private WealthManager wealthManager;
	[SerializeField] private UIController uiController;
	private bool canSpawn;

	[SerializeField] private SpriteRenderer houseSilhouettes;
	[SerializeField] private SpriteRenderer factorySilhouettes;
	private readonly Dictionary<BuildingType, SpriteRenderer> silhouettes = new();
	private SpriteRenderer selectedSilhouette;

	private void Awake()
	{
		input = new PlayerInput();
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	private void Start()
	{
		silhouettes.Add(BuildingType.House, houseSilhouettes);
		silhouettes.Add(BuildingType.Factory, factorySilhouettes);
	}

	private void Update()
	{
		if (activeBuildingType == null)
			return;

		var mousePos = GetMouseWorldPosition();
		canSpawn = CanSpawnBuilding(mousePos);
		PreviewSilhouette(mousePos, selectedSilhouette);

		if (input.Construction.MouseLeftClick.WasPerformedThisFrame())
		{
			if (!canSpawn)
				return;

			if (!PayBuildingCost())
				return;

			ConstructBuilding(mousePos);
			DisableSilhouette();
		}
	}

	private void EnableSilhouette()
	{
		selectedSilhouette = silhouettes[activeBuildingType.Type];
		selectedSilhouette.enabled = true;
	}

	private void DisableSilhouette()
	{
		if (selectedSilhouette != null)
		{
			selectedSilhouette.enabled = false;
			selectedSilhouette = null;
		}
	}

	private void PreviewSilhouette(Vector2 mousePos, SpriteRenderer silhouette)
	{
		silhouette.transform.position = mousePos;
		silhouette.color = canSpawn ? Constants.Colors.ValidBuildingSilhouette : Constants.Colors.InvalidBuildingSilhouette;
		silhouette.gameObject.SetActive(true);
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

		if (activeBuildingType == null)
		{
			DisableSilhouette();
		}
		else
		{
			EnableSilhouette();
		}
	}

	private Vector2 GetMouseWorldPosition()
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
		worldPos.z = 0f;
		return worldPos;
	}

	private bool CanSpawnBuilding(Vector2 pos)
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return false;

		var buildingCollider = activeBuildingType.Prefab.GetComponent<BoxCollider2D>();
		if (Physics2D.OverlapBox(pos + buildingCollider.offset, buildingCollider.size, 0))
			return false;

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
