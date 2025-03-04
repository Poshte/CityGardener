using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField]
	private Button startBtn;

	[SerializeField]
	private Button quitBtn;
	private void Start()
	{
		Time.timeScale = 1f;
	}

	public void OnStartClicked()
	{
		SceneController.Instance.LoadScene();
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
