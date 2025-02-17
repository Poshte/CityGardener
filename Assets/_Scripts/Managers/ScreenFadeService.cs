using System.Collections;
using UnityEngine;

public static class ScreenFadeService
{
	public static IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float fadeDuration)
	{
		canvasGroup.alpha = startAlpha;

		var elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}

		canvasGroup.alpha = targetAlpha;
	}
}