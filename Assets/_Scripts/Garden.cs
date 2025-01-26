using UnityEngine;
using UnityEngine.InputSystem;

public class Garden : MonoBehaviour
{
	[SerializeField]
	private GameObject interactObject;

	private Transform playerTrnasform;
	[SerializeField]
	private float interactDistance = 2.5f;

	private void Awake()
	{
		playerTrnasform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
	}

	private void Update()
	{
		if (interactObject == null)
			return;

		if (PlayerStandingNear())
		{
			interactObject.SetActive(true);

			if (Keyboard.current.eKey.wasPressedThisFrame)
			{
				Interact();

				//TODO
				//AudioManager.Instance.PlayOneShot(FMODEvents.Instance.InteractSound, this.transform.position);
			}
		}
		else
		{
			interactObject.SetActive(false);
		}
	}

	private void Interact()
	{
		Debug.Log("We are interacting");
	}

	private bool PlayerStandingNear()
	{
		return Vector2.Distance(playerTrnasform.position, gameObject.transform.position) < interactDistance;
	}
}
