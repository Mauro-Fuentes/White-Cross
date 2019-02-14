using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RPG.Characters
{
	public class WeaponSystem : MonoBehaviour
	{

		[SerializeField] float baseDamage = 10f;
		[SerializeField] WeaponConfig currentWeaponConfig;	// tipo ScriptableObject

		GameObject target;
		GameObject weaponObject;
		Animator animator;	
		Character character;

		const string DEFAULT_ATTACK = "DEFAULT ATTACK";
		const string ATTACK_TRIGGER = "Attack";

		float lastHitTime;

		Vector3 clickPoint;

		void Start () 
		{
			PutWeaponInHand(currentWeaponConfig);
			SetAttackAnimation();
			animator = GetComponent<Animator>();
			character = GetComponent<Character>();
		}

		public void PutWeaponInHand (WeaponConfig weaponToUse)
		{
			currentWeaponConfig = weaponToUse;

			var weaponPrefab = weaponToUse.GetWeaponPrefab();

			GameObject dominantHand = RequestDominantHand();
			
			Destroy(weaponObject); // empty hands

			weaponObject = Instantiate (weaponPrefab, dominantHand.transform);
			weaponObject.transform.localPosition = currentWeaponConfig.gripTransform.localPosition;
			weaponObject.transform.localRotation= currentWeaponConfig.gripTransform.localRotation;
		}

		//getter
		public WeaponConfig GetCurrentWeapon()
		{
			return currentWeaponConfig;
		}

		public void AttackTarget(GameObject targetToAttack)
        {
			target = targetToAttack;
			// TODO: coroutines
        }

        private void SetAttackAnimation()
        {
			animator = GetComponent<Animator>();	
			var animatorOverrideController = character.GetAnimatorOverrideController();			
			animator.runtimeAnimatorController = character.GetAnimatorOverrideController();	// el runtimeAnimatorController lo alojas en la variable Animator del scope de la clase.
			animatorOverrideController [DEFAULT_ATTACK] = currentWeaponConfig.GetAttackAnimClip(); 	// access animations list, finde the one called Default attack and change ir... override
        }

		private GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();	// we could have an array of dominant hands
			int numberOfDominantHands = dominantHands.Length;				// get the number of dominant hands in that array.
			
			// handle 0														// UnityEngine Assertions
			// Assert.AreNotEqual (numberOfDominantHands, 0, "not dominat hand found on PlayerMovement");
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts");

			// si llegamos hasta acá, es porque encontramos un DominantHand
			return dominantHands[0].gameObject;	// agarramos el primer objecto que encontramos en el array dominantHands
		}

        private void AttackTarget()
        {
			if (Time.time - lastHitTime > currentWeaponConfig.GetMinTimeBetweenHits())
            {
				SetAttackAnimation();
                animator.SetTrigger(ATTACK_TRIGGER);
                lastHitTime = Time.time;
            }
        }

        private float CalculateDamage()		// Check for additional WeaponConfig damage
        {
			// normal damage
			return baseDamage + currentWeaponConfig.GetAdditionalDamage();
        }

	}
}
