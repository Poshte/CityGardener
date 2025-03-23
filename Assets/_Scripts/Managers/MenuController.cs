using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Canvas mainMenuCanvas;
	[SerializeField] private Canvas levelsCanvas;

	private void Start()
	{
		Time.timeScale = 1f;
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
