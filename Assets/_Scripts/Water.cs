using UnityEngine;

public class Water : MonoBehaviour, IInteractable
{
	[SerializeField]
	private SpriteRenderer interactSprite;

	public void EnableSprite()
	{
		interactSprite.enabled = true;
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
	}

	public void Interact()
	{
		Debug.Log("We are interacting with water!");
	}
}
