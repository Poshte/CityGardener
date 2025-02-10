using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private static SceneController _instance;
	public static SceneController Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("SceneController is null");
			}

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

	private readonly float waitTime = 1f;

	private const int defaultSceneIndex = 1;

	private void LoadNextScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void LoadPreviousScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private IEnumerator LoadSceneAsynchronously(int index)
	{
		//fire BeforeSceneDestroyed event
		GameEvents.Instance.BeforeSceneDestroyed();

		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadSceneAsync(index);
	}

	public void LoadScene(int? index = null)
	{
		index ??= defaultSceneIndex;
		StartCoroutine(LoadSceneAsynchronously(index.Value));
	}

	//public void LoadData(GameData data)
	//{
	//	sceneIndex = data.sceneIndex;
	//}

	//public void SaveData(GameData data)
	//{
	//	var currentIndex = SceneManager.GetActiveScene().buildIndex;
	//	data.sceneIndex = currentIndex == 0 ? sceneIndex : currentIndex;
	//}
}
