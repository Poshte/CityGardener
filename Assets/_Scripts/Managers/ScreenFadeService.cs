using System.Collections;
using UnityEngine;

public static class ScreenFadeService
{
	private static readonly float menuFade = 1f;
	private static readonly float gameFade = 2f;
	private static float fadeDuration;

	public static IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, int buildIndex)
	{
		if (buildIndex == 0)
		{
			(targetAlpha, startAlpha) = (startAlpha, targetAlpha);
			fadeDuration = menuFade;
		}
		else
		{
			fadeDuration = gameFade;
		}

		canvasGroup.alpha = startAlpha;

		var elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		canvasGroup.alpha = targetAlpha;
	}
}