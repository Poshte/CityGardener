using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private readonly float waitTime = 1f;
	private const int defaultSceneIndex = 1;

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
	}

	private IEnumerator LoadSceneAsynchronously(int index)
	{
		SceneManager.LoadSceneAsync(index);
		yield return new WaitForSeconds(waitTime);
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
