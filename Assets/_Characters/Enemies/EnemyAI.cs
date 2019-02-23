using UnityEngine;
using System.Collections;
using System;

namespace RPG.Characters
{	
	[RequireComponent(typeof(HealthSystem))]
	[RequireComponent(typeof(Character))]
	[RequireComponent(typeof(WeaponSystem))]
	public class EnemyAI : MonoBehaviour
	{

		[SerializeField] float chaseRadious = 6f;
		[SerializeField] WaypointContainer patrolPath;
		[SerializeField] float waypointTolerance = 2f;
		
		PlayerController player;
		Character character;
		
		//bool isAttacking = false;

		float currentWeaponRange;
		float distanceToPlayer;
		int nextWaypointIndex;
		

		enum State	{ idle, attacking, patrolling, chasing }
		State state = State.idle;

		void Start()
		{
			player = FindObjectOfType <PlayerController>();		// Find the player and fetchit
			character = GetComponent<Character>();
		}

		void Update()
		{

		 	distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
			
			WeaponSystem weaponSystem = GetComponent<WeaponSystem>();
			
			currentWeaponRange = weaponSystem.GetCurrentWeapon().GetMaxAttackRange();

			bool inWeaponCircle = distanceToPlayer <= currentWeaponRange;
			bool inChaseCircle = distanceToPlayer > currentWeaponRange && distanceToPlayer <= chaseRadious;
			bool outsideChaseCircle = distanceToPlayer > chaseRadious;

			if (outsideChaseCircle)
			{
				StopAllCoroutines();
				weaponSystem.StopAttacking();
				StartCoroutine( Patrol() );
			}
			if (inChaseCircle)
			{
				StopAllCoroutines();
				weaponSystem.StopAttacking();
				StartCoroutine(ChasePlayer());
			}
			if (inWeaponCircle)
			{
				StopAllCoroutines();

				state = State.attacking;
				//state = State.attacking; si activo deja de funcionar el health del player
				weaponSystem.AttackTarget(player.gameObject);
			}

////////////////////////////////////////////////////
			// if (distanceToPlayer > chaseRadious && state != State.patrolling)
			// {
			// 	StopAllCoroutines();
			// 	weaponSystem.StopAttacking();
			// 	StartCoroutine( Patrol() );
			// }
			// if (distanceToPlayer <= chaseRadious && state != State.chasing)
			// {
			// 	StopAllCoroutines();
			// 	weaponSystem.StopAttacking();
			// 	StartCoroutine(ChasePlayer());
			// }
			// if (distanceToPlayer <= currentWeaponRange && state != State.attacking)
			// {
			// 	StopAllCoroutines();
			// 	//state = State.attacking; si activo deja de funcionar el health del player
			// 	weaponSystem.AttackTarget(player.gameObject);
			// }
///////////////////////////////////////////////////////
		}

		IEnumerator Patrol()
		{
			state = State.patrolling;

			while (patrolPath != null)
			{
				Vector3 nextWaypointPos = patrolPath.transform.GetChild(nextWaypointIndex).position;
	
				character.SetDestination(nextWaypointPos);
			
				CycleWaypointWhenClose(nextWaypointPos);

				yield return new WaitForSeconds(waypointTolerance);
			}
			
		}

		IEnumerator  ChasePlayer ()
		{
			state = State.chasing;
			while (distanceToPlayer >= currentWeaponRange)
			{
				character.SetDestination(player.transform.position);
				yield return new WaitForEndOfFrame();
			}
		}



        private void CycleWaypointWhenClose(Vector3 nextWaypointPos)
        {
            if (Vector3.Distance (transform.position, nextWaypointPos) <= waypointTolerance)
			{
				nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
			}
			
        }

        void OnDrawGizmos()
		{
			// ATTACKRADIOUS GIZMOS RED
			Gizmos.color = new Color (255f, 0f, 0, .5f); 
			Gizmos.DrawWireSphere (transform.position, currentWeaponRange);

			// CHACERAIOUS GIZMOS BLUE
			Gizmos.color = new Color (0f, 0f, 255f, .5f); 
			Gizmos.DrawWireSphere (transform.position, chaseRadious);
		}	
	}
}