using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
	private CanvasGroup canvasGroup;
	private float startAlpha = 1f;
	private float targetAlpha = 0f;
	[SerializeField] private bool reverse;
	[SerializeField] private float fadeDuration;

	private void Awake()
	{
		canvasGroup = gameObject.GetComponent<CanvasGroup>();
	}

	private void OnEnable()
	{
		if (reverse)
			(startAlpha, targetAlpha) = (targetAlpha, startAlpha);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		GameEvents.Instance.OnBeforeSceneDestroyed += BeforeSceneDestroyed;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		StartCoroutine(ScreenFadeService.Fade(canvasGroup, startAlpha, targetAlpha, fadeDuration));
	}

	private void BeforeSceneDestroyed()
	{
		StartCoroutine(ScreenFadeService.Fade(canvasGroup, targetAlpha, startAlpha, fadeDuration));
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		GameEvents.Instance.OnBeforeSceneDestroyed -= BeforeSceneDestroyed;
	}
}
