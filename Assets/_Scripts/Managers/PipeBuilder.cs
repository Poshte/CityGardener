using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PipeBuilder : MonoBehaviour
{
	private PipeSO activePipe;
	private PlayerInput input;

	private Vector2 startingPosition;
	private Vector2 endingPosition;
	private bool canSpawn;

	[SerializeField] private Transform prefab;
	[SerializeField] private SpriteRenderer selectedSilhouette;

	private float prefabWidth;
	private Vector2 direction;
	private const float Pipes_Spacing = 0.1f;
	private const float Pipe_SearchRadius = 0.5f;
	private int numberOfInstances;
	private readonly List<Transform> previewObjects = new();

	private WealthManager wealthManager;

	private void Awake()
	{
		input = new PlayerInput();
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	private void Start()
	{
		prefabWidth = prefab.GetComponent<Renderer>().bounds.size.x;
	}

	private void Update()
	{
		if (activePipe == null)
			return;

		//clear up everything
		if (input.Construction.MouseRightClick.WasPerformedThisFrame())
		{
			RemovePreviewObjects();
			ClearUp();
		}

		//when a pipe is selected
		var mousePos = Helper.GetMouseWorldPosition();
		canSpawn = CanSpawn(mousePos);
		PreviewSilhouette(mousePos);

		//spawn the preview after first click
		if (startingPosition != Vector2.zero)
		{
			SpawnPreview(mousePos);
		}

		if (input.Construction.MouseLeftClick.WasPerformedThisFrame())
		{
			//on first click
			//only store click position
			if (startingPosition == Vector2.zero)
			{
				if (!canSpawn)
					return;

				startingPosition = mousePos;
				return;
			}

			//on second click
			//finalize building of pipes
			if (!PayPipesCost())
				return;

			FinalizeSpawn();
		}
	}

	private bool PayPipesCost()
	{
		return wealthManager.SpendWealth(activePipe.Cost * numberOfInstances);
	}

	private bool CanSpawn(Vector2 position)
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return false;

		var hitColliders = Physics2D.OverlapCircleAll(position, Pipe_SearchRadius);
		return hitColliders.Any(c => c.CompareTag(Constants.Tags.WaterResource) || c.CompareTag(Constants.Tags.Pipe));
	}

	private void EnableSilhouette()
	{
		selectedSilhouette.enabled = true;
	}

	private void DisableSilhouette()
	{
		selectedSilhouette.enabled = false;
	}

	private void PreviewSilhouette(Vector2 mousePos)
	{
		selectedSilhouette.transform.position = mousePos;
		selectedSilhouette.color = canSpawn ? Constants.Colors.ValidBuildingSilhouette : Constants.Colors.InvalidBuildingSilhouette;
	}

	private void SpawnPreview(Vector2 mousePos)
	{
		RemovePreviewObjects();

		endingPosition = mousePos;
		direction = (endingPosition - startingPosition).normalized;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		var distance = Vector2.Distance(startingPosition, endingPosition);
		numberOfInstances = Mathf.FloorToInt(distance / prefabWidth + Pipes_Spacing);

		for (int i = 0; i < numberOfInstances; i++)
		{
			var pos = startingPosition + direction * (i * (prefabWidth + Pipes_Spacing));
			previewObjects.Add(Instantiate(prefab, pos, Quaternion.Euler(0, 0, angle)));
		}
	}

	private void RemovePreviewObjects()
	{
		foreach (var obj in previewObjects)
		{
			Destroy(obj.gameObject);
		}

		previewObjects.Clear();
	}

	private void FinalizeSpawn()
	{
		foreach (var obj in previewObjects)
		{
			obj.tag = Constants.Tags.Pipe;
			obj.GetComponent<SpriteRenderer>().color = Color.black;
		}

		//this will allow player continue building pipes in a new direction
		var lastPipePos = (Vector2)previewObjects.Last().position;
		startingPosition = lastPipePos + direction * ((prefabWidth + Pipes_Spacing) * 0.5f);

		previewObjects.Clear();
	}

	private void ClearUp()
	{
		startingPosition = endingPosition = Vector2.zero;
		activePipe = null;
		DisableSilhouette();
	}

	private void OnEnable()
	{
		input.Construction.Enable();
	}

	private void OnDisable()
	{
		input.Construction.Disable();
	}

	public void SetActivePipe(PipeSO pipeSO)
	{
		activePipe = pipeSO;

		if (activePipe == null)
		{
			RemovePreviewObjects();
			ClearUp();
		}
		else
		{
			EnableSilhouette();
		}
	}
}
