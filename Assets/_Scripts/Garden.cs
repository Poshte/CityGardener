using UnityEngine;
using UnityEngine.InputSystem;

public class Garden : MonoBehaviour
{
	[SerializeField]
	private GameObject interactObject;

	[SerializeField]
	private Tree treePrefab;
	private Tree tree;

	private Transform playerTrnasform;
	[SerializeField]
	private float interactDistance = 2.5f;

	private bool interactable;

	private void Awake()
	{
		playerTrnasform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
	}

	private void Update()
	{
		if (interactObject == null)
			return;

		CheckInteraction();

		if (interactable && Keyboard.current.eKey.wasPressedThisFrame)
		{
			Interact();

			//TODO
			//AudioManager.Instance.PlayOneShot(FMODEvents.Instance.InteractSound, this.transform.position);
		}
	}

	private void CheckInteraction()
	{
		if (PlayerStandingNear())
		{
			if (tree == null)
			{
				//TODO
				//set plant interaction object
				interactObject.SetActive(true);
				interactable = true;
			}
			else if (tree.NeedsWater)
			{
				//TODO
				//set pour water interaction object
				interactObject.SetActive(true);
				interactable = true;
			}
		}
		else
		{
			ResetInteraction();
		}
	}

	private void ResetInteraction()
	{
		interactObject.SetActive(false);
		interactable = false;
	}

	private void Interact()
	{
		if (tree == null)
		{
			//plant
			var pos = this.transform.position;
			pos.y += 0.75f;
			tree = Instantiate(treePrefab, pos, Quaternion.identity);

			Debug.Log("Planted Tree. " + tree.GrowthState);
		}
		else if (tree.NeedsWater)
		{
			//pour water
			tree.Grow();
			Debug.Log("Watering Tree. " + tree.GrowthState);
			ResetInteraction();
		}
	}

	private bool PlayerStandingNear()
	{
		return Vector2.Distance(playerTrnasform.position, gameObject.transform.position) < interactDistance;
	}
}
