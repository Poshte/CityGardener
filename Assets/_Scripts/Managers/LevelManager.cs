using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private LevelSO levelGoals;
	[SerializeField] private TextMeshProUGUI pollutionGoalsUI;
	[SerializeField] private RectTransform treeGoalsParent;
	[SerializeField] private Goal treeGoalPrefab;

	private readonly Dictionary<Goal, string> treeGoalsCount = new();

	private void Start()
	{
		GameEvents.Instance.OnTreePlanted += OnTreePlanted;
		InitializeLevelGoals();
	}

	private void OnTreePlanted(TreeType tree)
	{
		UpdateTreeGoalCount(tree);
	}

	private void UpdateTreeGoalCount(TreeType tree)
	{
		var pair = treeGoalsCount.FirstOrDefault(g => g.Key.Id == tree);

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
			CheckWinCondition();
		}

		treeGoalsCount.Remove(goal);
		treeGoalsCount.Add(goal, countText);
	}

	private void CheckWinCondition()
	{
		if (treeGoalsCount.All(g => g.Key.FulfilledUI.isOn))
			WinLevel();
	}

	private void WinLevel()
	{
		Debug.Log("YOU WON!!!!");
		//TODO
		//show win panel in UI
	}

	private void InitializeLevelGoals()
	{
		GetPollutionGoal();
		GetTreeGoals();
	}

	private void GetTreeGoals()
	{
		var groupedTrees = levelGoals.TreesToPlant.GroupBy(t => t)
			.Select(g => new { Type = g.Key, Count = g.Count() });

		var pos = treeGoalsParent.position;

		foreach (var tree in groupedTrees)
		{
			var treeGoal = Instantiate(treeGoalPrefab, treeGoalsParent);
			pos.y -= 30;
			treeGoal.transform.position = pos;

			treeGoal.Id = tree.Type;

			var uiText = $"  {tree.Count} {tree.Type} " + (tree.Count == 1 ? "Tree" : "Trees");
			treeGoal.DescriptionUI.text = uiText;

			var countText = $"0/{tree.Count}";
			treeGoal.CountUI.text = countText;

			treeGoalsCount.Add(treeGoal, countText);
		}
	}

	private void GetPollutionGoal()
	{
		pollutionGoalsUI.text = "- Reduce the amount of pollution to " + levelGoals.PollutionGoal + " index";
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnTreePlanted -= OnTreePlanted;
	}
}
