using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private LevelSO levelGoals;
	[SerializeField] private TextMeshProUGUI treeGoalsUI;
	[SerializeField] private TextMeshProUGUI pollutionGoalsUI;

	private void Start()
	{
		UpdateLevelGoalsUI();
	}

	private void UpdateLevelGoalsUI()
	{
		pollutionGoalsUI.text = GetPollutionText();
		treeGoalsUI.text = GetTreeText();
	}

	private string GetTreeText()
	{
		var groupedTrees = levelGoals.TreesToPlant.GroupBy(t => t)
			.Select(g => new { Type = g.Key, Count = g.Count() });

		var uiText = Constants.PredefinedTexts.TreeUIText;
		foreach (var tree in groupedTrees)
		{
			uiText += Environment.NewLine;
			uiText += $"{tree.Count} {tree.Type} Trees";
		}

		return uiText;
	}

	private string GetPollutionText()
	{
		return Constants.PredefinedTexts.PollutionUIText + " " + levelGoals.PollutionGoal + " " + "index";
	}
}
