using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI pollutionGoalsUI;
	[SerializeField] private RectTransform treeGoalsParent;
	[SerializeField] private Goal treeGoalPrefab;

	private readonly Dictionary<Goal, string> treeGoalsCount = new();

	private bool treeGoalsFulfilled;
	private PollutionManager pollutionManager;

	private LevelsHub levelsHub;
	private Level currentLevel;

	private InventoryManager inventoryManager;
	private UIController uiController;

	private void Awake()
	{
		levelsHub = GameObject.FindGameObjectWithTag(Constants.Tags.LevelsHub).GetComponent<LevelsHub>();
		pollutionManager = GameObject.FindGameObjectWithTag(Constants.Tags.PollutionManager).GetComponent<PollutionManager>();
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		uiController = GameObject.FindGameObjectWithTag(Constants.Tags.UIController).GetComponent<UIController>();
	}

	private void Start()
	{
		Time.timeScale = 1f;

		currentLevel = levelsHub.Levels.FirstOrDefault(l => l.GameScene == (GameScene)SceneManager.GetActiveScene().buildIndex);
		InitializeLevel();
		InitializeGoals();
	}

	private void Update()
	{
		if (treeGoalsFulfilled)
		{
			if (PollutionGoalWinConditionFulfilled())
			{
				GameEvents.Instance.WinLevel();
			}
		}
	}

	private void InitializeLevel()
	{
		foreach (var invItem in currentLevel.InventoryItems)
		{
			inventoryManager.AddItem(invItem);
		}

		foreach (var actItem in currentLevel.ActionBarItems)
		{
			uiController.AddActionBarItem(actItem);
		}
	}

	public void UnlockNextLevel()
	{
		var nextLvl = levelsHub.Levels.FirstOrDefault(l => l.GameScene == (GameScene)SceneManager.GetActiveScene().buildIndex + 1);
		nextLvl.Unlocked = true;
	}

	private bool PollutionGoalWinConditionFulfilled()
	{
		return currentLevel.PollutionGoal > pollutionManager.GetCurrentPollutionIndex();
	}

	private void OnMatureTreePlanted(TreeType tree)
	{
		UpdateTreeGoalCount(tree);
	}

	private void UpdateTreeGoalCount(TreeType tree)
	{
		var pair = treeGoalsCount.FirstOrDefault(g => g.Key.TypeId == tree);

		var goal = pair.Key;
		var countTextElements = pair.Value.Split('/');

		if (goal.FulfilledUI.isOn)
			return;

		int.TryParse(countTextElements[0], out int count);
		int.TryParse(countTextElements[1], out int total);

		var countText = $"{++count}/{total}";
		goal.CountUI.text = countText;

		if (count == total)
		{
			goal.FulfilledUI.isOn = true;
			CheckTreeGoalsWinCondition();
		}

		treeGoalsCount.Remove(goal);
		treeGoalsCount.Add(goal, countText);
	}

	private void CheckTreeGoalsWinCondition()
	{
		if (treeGoalsCount.All(g => g.Key.FulfilledUI.isOn))
			treeGoalsFulfilled = true;
	}

	private void InitializeGoals()
	{
		GetPollutionGoal();
		GetTreeGoals();
	}

	private void GetTreeGoals()
	{
		var groupedTrees = currentLevel.TreesToPlant.GroupBy(t => t)
			.Select(g => new { Type = g.Key, Count = g.Count() });

		var pos = treeGoalsParent.position;

		foreach (var tree in groupedTrees)
		{
			var treeGoal = Instantiate(treeGoalPrefab, treeGoalsParent);
			pos.y -= 30;
			treeGoal.transform.position = pos;

			treeGoal.TypeId = tree.Type;

			var uiText = $"  {tree.Count} {tree.Type} " + (tree.Count == 1 ? "Tree" : "Trees");
			treeGoal.DescriptionUI.text = uiText;

			var countText = $"0/{tree.Count}";
			treeGoal.CountUI.text = countText;

			treeGoalsCount.Add(treeGoal, countText);
		}
	}

	private void GetPollutionGoal()
	{
		pollutionGoalsUI.text = "- Reduce the amount of pollution to " + currentLevel.PollutionGoal + " index";
	}

	private void OnEnable()
	{
		GameEvents.Instance.OnMatureTreePlanted += OnMatureTreePlanted;
		GameEvents.Instance.OnWinningLevel += UnlockNextLevel;
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnMatureTreePlanted -= OnMatureTreePlanted;
		GameEvents.Instance.OnWinningLevel -= UnlockNextLevel;
	}

	private void OnDestroy()
	{
		GameManager.Reset();
	}
}
