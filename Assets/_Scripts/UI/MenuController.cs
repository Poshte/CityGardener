using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Canvas mainMenuCanvas;
	[SerializeField] private Canvas levelsCanvas;

	[SerializeField] private Button[] buttons;
	public readonly Dictionary<Button, Level> LevelButtons = new();

	private LevelsHub levelsHub;

	private void Awake()
	{
		levelsHub = GameObject.FindGameObjectWithTag(Constants.Tags.LevelsHub).GetComponent<LevelsHub>();
	}

	private void Start()
	{
		Time.timeScale = 1f;

		Initialize();
		MakeUnlockedLevelsInteractable();
	}

	private void Initialize()
	{
		for (int i = 0; i < levelsHub.Levels.Length; i++)
		{
			LevelButtons.Add(buttons[i], levelsHub.Levels[i]);
		}
	}

	private void MakeUnlockedLevelsInteractable()
	{
		foreach (var pair in LevelButtons)
		{
			if (pair.Value.Unlocked)
			{
				pair.Key.interactable = true;
			}
		}
	}

	public void OnLevel_Clicked(int levelIndex)
	{
		SceneController.Instance.LoadScene(levelIndex);
	}

	public void OnBack_Clicked()
	{
		mainMenuCanvas.enabled = true;
		levelsCanvas.enabled = false;
	}

	public void OnStartClicked()
	{
		mainMenuCanvas.enabled = false;
		levelsCanvas.enabled = true;
	}

	public void OnQuitClicked()
	{
#if UNITY_STANDALONE
		Application.Quit();
#endif

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
