using UnityEngine;

namespace RPG.Characters
{	
	
	[RequireComponent(typeof(WeaponSystem))]
	public class EnemyAI : MonoBehaviour
	{

		[SerializeField] float chaceRadious = 6f;
		PlayerMovement player;
		
		bool isAttacking = false; // TODO: rich state

		float currentWeaponRange;

		void Start()
		{
			player = FindObjectOfType <PlayerMovement>();		// Find the player and fetchit
		}

		void Update()
		{
			WeaponSystem weaponSystem = GetComponent<WeaponSystem>();

			float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);

			currentWeaponRange = weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
		}

		void OnDrawGizmos()
		{
			// ATTACKRADIOUS GIZMOS RED
			Gizmos.color = new Color (255f, 0f, 0, .5f); 
			Gizmos.DrawWireSphere (transform.position, currentWeaponRange);

			// CHACERAIOUS GIZMOS BLUE
			Gizmos.color = new Color (0f, 0f, 255f, .5f); 
			Gizmos.DrawWireSphere (transform.position, chaceRadious);
		}	
	}
}