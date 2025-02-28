using UnityEngine;

public class Water : MonoBehaviour, IInteractable
{
	[SerializeField]
	private SpriteRenderer interactSprite;

	public void EnableInteraction()
	{
		interactSprite.enabled = true;
		GameManager.NearWater = true;
	}

	public void DisableInteraction()
	{
		interactSprite.enabled = false;
		GameManager.NearWater = false;
	}
}
