using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField]
	private Button startBtn;

	[SerializeField]
	private Button quitBtn;

	public void OnStartClicked()
	{
		SceneController.Instance.LoadScene();
	}

	public void OnQuitClicked()
	{
		SceneController.Instance.LoadScene();
	}
}
