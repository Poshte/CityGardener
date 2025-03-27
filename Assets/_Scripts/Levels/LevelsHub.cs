using UnityEngine;

public class LevelsHub : MonoBehaviour
{
	public Level[] Levels { get => _levels; set => _levels = value; }
	[SerializeField] private Level[] _levels;

	private void Start()
	{
		DontDestroyOnLoad(this);
	}
}
