using System.Linq;
using UnityEngine;

public class EarthTill : MonoBehaviour
{
	private PlayerInput input;
	[SerializeField] private Transform gardenPrefab;

	private Player player;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
		input = new PlayerInput();
	}

	private void Update()
	{
		if (player.IsWalking)
		{
			input.Interaction.Till.Reset();
			return;
		}

		if (input.Interaction.Till.WasPressedThisFrame())
		{
			if (GameManager.NearbyGardens.Any())
			{
				input.Interaction.Till.Reset();
				return;
			}

			//play tilling animation
			//until Till is performed
			Debug.Log("Tilling...");
		}

		if (input.Interaction.Till.WasPerformedThisFrame())
		{
			Instantiate(gardenPrefab, transform.position, Quaternion.identity);

			//stop animation
			return;
		}

		if (input.Interaction.Till.WasReleasedThisFrame())
		{
			//stop animation
		}
	}

	private void OnEnable()
	{
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Interaction.Disable();
	}
}
