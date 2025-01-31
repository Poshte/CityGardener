using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
	private static GameEvents _instance;
	public static GameEvents Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}
}
