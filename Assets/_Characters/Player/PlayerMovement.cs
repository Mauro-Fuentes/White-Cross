using UnityEngine;
using UnityEngine.Assertions;

using RPG.CameraUI; // for mouse events

namespace RPG.Characters
{
	public class PlayerMovement : MonoBehaviour
	{

		[Range (.1f, 1.0f)] [SerializeField] float criticalHitChange = 0.1f;
		[SerializeField] float criticalHitMultiplier = 1.25f;
		[SerializeField] ParticleSystem criticalHitParticles = null;			

		EnemyAI EnemyAI;
		CameraRaycaster cameraRaycaster;
		SpecialAbilities abilities;
		Character character;
		WeaponSystem weaponSystem;

		void Start ()
        {
			character = GetComponent<Character>();
			abilities = GetComponent<SpecialAbilities>();
			weaponSystem = GetComponent<WeaponSystem>();

			RegisterForMouseEvent();
        }

		private void RegisterForMouseEvent()
		{
			cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
			cameraRaycaster.onMouseOverPotenciallyWalkable += onMouseOverPotenciallyWalkable;
		}

        void Update()
		{
			ScanForAbilityKeyDown();
		}

		private void ScanForAbilityKeyDown()
		{
			// keyIndex starts at 1, keyIndex is never greater than abilities array, increment 1
			for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex ++)
			{
				if (Input.GetKeyDown(keyIndex.ToString()))
				{
					// attempt the abillity according to the keyIndex 
					abilities.AttemptSpecialAbility(keyIndex);
				}
			}
		}

		void onMouseOverPotenciallyWalkable(Vector3 destination)
		{
			if (Input.GetMouseButton(0))
			{
				character.SetDestination(destination);
			}
		}

		void OnMouseOverEnemy(EnemyAI enemyToSet)
		{
			// set current EnemyAI
			this.EnemyAI = enemyToSet;

			if (Input.GetMouseButton(0) && IsTargetInRange (EnemyAI.gameObject))
			{
				weaponSystem.AttackTarget(EnemyAI.gameObject);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				abilities.AttemptSpecialAbility( 0);
			}
		}

        bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
			return distanceToTarget <=  weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }

	}
}