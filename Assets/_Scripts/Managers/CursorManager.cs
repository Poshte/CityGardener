using UnityEngine;

public class CursorManager : MonoBehaviour
{
	[SerializeField] private Texture2D cursor;

	private void Start()
	{
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		Cursor.visible = true;
	}
}
