using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

using RPG.CameraUI; // for mouse events

namespace RPG.Characters
{
	
	public class PlayerController : MonoBehaviour
	{	
		
		SpecialAbilities abilities;
		Character character;
		WeaponSystem weaponSystem;
		OpenChest openChest;	//TODO: //change name

		void Start ()
        {
			character = GetComponent<Character>();
			abilities = GetComponent<SpecialAbilities>();
			weaponSystem = GetComponent<WeaponSystem>();
			openChest = FindObjectOfType<OpenChest>();

			RegisterForMouseEvent();

        }

		private void OnMouseOverCollectable(Interactable item)
		{
			if (Input.GetMouseButton(0))
			{
				character.SetDestination(item.transform.position); //TODO: // verificar si es buena idea ir hacia el interactable transform

			}
			else if (Input.GetMouseButton(0))
			{
				//StartCoroutine ();
			}
		}


		private void RegisterForMouseEvent()
		{
			CameraRaycaster cameraRaycaster;

			cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
			cameraRaycaster.onMouseOverPotenciallyWalkable += onMouseOverPotenciallyWalkable;
			cameraRaycaster.onMouseOverCollectables += OnMouseOverCollectable;
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
				weaponSystem.StopAttacking();
				character.SetDestination(destination);
			}
		}

		bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
			return distanceToTarget <=  weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }

		void OnMouseOverEnemy(EnemyAI enemy)
		{

			if (Input.GetMouseButton(0) && IsTargetInRange (enemy.gameObject))
			{
				weaponSystem.AttackTarget(enemy.gameObject);
			}
			else if (Input.GetMouseButton(0) && !IsTargetInRange (enemy.gameObject))
			{
				StartCoroutine (MoveAndAttack (enemy));
			}
			else if (Input.GetMouseButtonDown(1) && IsTargetInRange (enemy.gameObject) )
			{
				abilities.AttemptSpecialAbility(0, enemy.gameObject);
			}
			else if (Input.GetMouseButtonDown(1) && !IsTargetInRange (enemy.gameObject) )
			{
				StartCoroutine (MoveAndPowerAttack (enemy));
			}
		}

		IEnumerator MoveAndAttack (EnemyAI enemy)
		{
			yield return StartCoroutine (MoveToTarget (enemy.gameObject));
			weaponSystem.AttackTarget(enemy.gameObject);
		}

		IEnumerator MoveAndPowerAttack (EnemyAI enemy)
		{
			yield return StartCoroutine (MoveToTarget (enemy.gameObject));
			abilities.AttemptSpecialAbility(0, enemy.gameObject);
		}



		IEnumerator MoveToTarget (GameObject target)
		{
			character.SetDestination(target.transform.position);

			while (!IsTargetInRange(target))
			{
				yield return new WaitForEndOfFrame();
			}

			yield return new WaitForEndOfFrame();
		}
	}
}