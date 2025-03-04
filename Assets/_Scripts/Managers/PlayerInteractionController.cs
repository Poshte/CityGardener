using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
	private PlayerInput input;
	private bool isGamePaused;

	[SerializeField] private GameObject pauseCanvas;
	[SerializeField] private Canvas uiCanvas;

	[SerializeField] private Button resumeBtn;
	[SerializeField] private Button quitBtn;

	private void Awake()
	{
		input = new PlayerInput();
	}

	private void Update()
	{
		if (input.Interaction.Reset.WasPerformedThisFrame())
		{
			//TODO
			//add visual for resetting
			Debug.Log("Resetting...");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (input.Interaction.Pause.WasPerformedThisFrame())
		{
			PerformPausingFunction();
		}
	}

	public void OnResumeClicked()
	{
		PerformPausingFunction();
	}

	public void OnQuitClicked()
	{
		SceneController.Instance.LoadScene(0);
		pauseCanvas.SetActive(false);
		isGamePaused = false;
		Time.timeScale = 1f;

		//PerformPausingFunction();
	}

	private void PerformPausingFunction()
	{
		//pausing the game
		if (!isGamePaused)
		{
			Time.timeScale = 0f;
			pauseCanvas.SetActive(true);
			uiCanvas.enabled = false;
			isGamePaused = true;
		}
		//unpausing the game
		else
		{
			Time.timeScale = 1f;
			pauseCanvas.SetActive(false);
			uiCanvas.enabled = true;
			isGamePaused = false;
		}
	}

	private void OnEnable()
	{
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Interaction.Disable();
	}
}
