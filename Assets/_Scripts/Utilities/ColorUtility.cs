using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public static class ColorUtility
{
	public static IEnumerator ChangeColor(TextMeshProUGUI text, Color color, float speed)
	{
		var original = text.color;

		var elapsedTime = 0f;
		while (elapsedTime < speed)
		{
			text.color = Color.Lerp(color, original, elapsedTime / speed);
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}

		text.color = original;
	}

	public static IEnumerator Blink(TextMeshProUGUI text, Color color, float duration, int count)
	{
		var original = text.color;
		var blinkDuration = duration / (count * 2);

		for (int i = 0; i < count; i++)
		{
			text.color = color;
			yield return new WaitForSeconds(blinkDuration);

			text.color = original;
			yield return new WaitForSeconds(blinkDuration);
		}

		text.color = original;
	}
}
