using UnityEngine;
using UnityEngine.UI;

public class GameConditionController : MonoBehaviour
{
	[SerializeField] private CanvasGroup lostGameCanvasGroup;
	[SerializeField] private CanvasGroup wonGameCanvasGroup;

	[SerializeField] private CanvasGroup UiCanvas;
	[SerializeField] private float fadeDuration;

	private void OnEnable()
	{
		GameEvents.Instance.OnWinningLevel += WinLevel;
		GameEvents.Instance.OnLosingLevel += LoseLevel;
	}

	private void WinLevel()
	{
		Time.timeScale = 0f;
		StartCoroutine(ScreenFadeService.Fade(UiCanvas, 1f, 0f, fadeDuration));

		wonGameCanvasGroup.gameObject.SetActive(true);
		StartCoroutine(ScreenFadeService.Fade(wonGameCanvasGroup, 0f, 1f, fadeDuration));
	}

	private void LoseLevel()
	{
		Time.timeScale = 0f;
		StartCoroutine(ScreenFadeService.Fade(UiCanvas, 1f, 0f, fadeDuration));
		StartCoroutine(ScreenFadeService.Fade(lostGameCanvasGroup, 0f, 1f, fadeDuration));
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnWinningLevel -= WinLevel;
		GameEvents.Instance.OnLosingLevel -= LoseLevel;
	}
}