using UnityEngine;

public class LevelsHub : MonoBehaviour
{
	public Level[] Levels { get => levels; set => levels = value; }
	[SerializeField] private Level[] levels;

	private void Start()
	{
		DontDestroyOnLoad(this);
	}
}
