using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private Canvas canvas;

	private void Start()
	{
		canvas = GetComponent<Canvas>();
	}

	public void OnCloseClicked()
    {
        canvas.enabled = false;
    }
}
