using UnityEngine;

public class Water : MonoBehaviour, IInteractable
{
	[SerializeField]
	private SpriteRenderer interactSprite;

	public void EnableSprite()
	{
		interactSprite.enabled = true;
		GameManager.NearWater = true;
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.NearWater = false;
	}
}
