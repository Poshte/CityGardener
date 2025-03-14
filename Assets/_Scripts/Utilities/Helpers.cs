using UnityEngine.InputSystem;
using UnityEngine;

public static class Helper
{
	public static Vector2 GetMouseWorldPosition()
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
		worldPos.z = 0f;
		return worldPos;
	}
}