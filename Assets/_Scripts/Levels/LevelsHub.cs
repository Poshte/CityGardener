using UnityEngine;

public class LevelsHub : MonoBehaviour
{
	public Level[] Levels;

	private void Start()
	{
		DontDestroyOnLoad(this);
	}
}
