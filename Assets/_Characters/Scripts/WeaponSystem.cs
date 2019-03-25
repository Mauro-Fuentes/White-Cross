using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace RPG.Characters
{
	public class WeaponSystem : MonoBehaviour
	{
	
		[SerializeField] float baseDamage = 10f;
		[SerializeField] WeaponConfig currentWeaponConfig;	// tipo ScriptableObject

		public float speed = 200f;

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
			animator = GetComponent<Animator>();
			character = GetComponent<Character>();

			SetAttackAnimation();
		}

		void Update()
		{
			bool targetIsDead;	// inicialise these pair of bools
			bool targetIsOutOfRange;

			// if there is no target, those bools are false. Neither the target is dear or out of range.
			if (target == null)
			{
				targetIsDead = false;
				targetIsOutOfRange = false;				
			}
			// if they are in range...
			else	
			{
				// test if target is dead
				// cash the target health acording to the HealthSystem.cs
				var targetHealth = target.GetComponent<HealthSystem>().healthAsPercentage;
				targetIsDead = targetHealth <= Mathf.Epsilon;

				// testi if our of range

				var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
 				targetIsOutOfRange = distanceToTarget > currentWeaponConfig.GetMaxAttackRange();
			}

			float characterHealth = GetComponent<HealthSystem>().healthAsPercentage;
			bool characterIsDead = (characterHealth <= Mathf.Epsilon);

			if (characterIsDead || targetIsOutOfRange || targetIsDead)
			{
				StopAllCoroutines();
			}
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
			StartCoroutine (AttackTargetRepeatedly());
			
        }

		public void StopAttacking()
		{
			animator.StopPlayback();
			StopAllCoroutines();
		}

		IEnumerator AttackTargetRepeatedly()
		{
			
			// determine if alive
			bool attackerStillAlive = GetComponent<HealthSystem>().healthAsPercentage >= Mathf.Epsilon;
			bool targetStillAlive = target.GetComponent<HealthSystem>().healthAsPercentage >= Mathf.Epsilon;

			while (attackerStillAlive && targetStillAlive)
			{
				// take the current animation of that weapon
				var animationClip = currentWeaponConfig.GetAttackAnimClip();
				// how long is the animation
				float animationClipTime = animationClip.length / character.GetAnimSpeedMultiplier();
				// what's the time
				float timeToWait = animationClipTime + currentWeaponConfig.GetTimeBetweenCycles();

				bool isTimeToHitAgain = Time.time - lastHitTime > timeToWait;

				if (isTimeToHitAgain)
				{
					AttackTargetOnce();
					lastHitTime = Time.time;
				}

				yield return new WaitForSeconds(timeToWait);
			}

		}

        void AttackTargetOnce()
        {
            transform.LookAt(target.transform);

			animator.SetTrigger(ATTACK_TRIGGER);

			float damageDelay = currentWeaponConfig.GetDamageDelay();

			SetAttackAnimation();

			StartCoroutine (DamageAfterDelay (damageDelay));

			GetWeaponFXPrefab();
			
        }

		public void GetWeaponFXPrefab()
		{
			var spawnpoint = currentWeaponConfig.gripTransform.transform;
			// Debug.Log (spawnpoint.transform.position);
			// Debug.Log (spawnpoint.transform.localPosition);
			
			var prefab = currentWeaponConfig.GetParticlePrefab();
			
			var clone = Instantiate (prefab, spawnpoint.transform, false);

 			clone.transform.parent = gameObject.transform;
			clone.transform.position = gameObject.transform.position;
			clone.transform.LookAt(target.transform);

		}
        

        IEnumerator DamageAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
			target.GetComponent<HealthSystem>().TakeDamage(CalculateDamage());
        }

        void SetAttackAnimation()
        {
			if (!character.GetAnimatorOverrideController())
			{
				Debug.Break();
				Debug.LogAssertion ("Please provide" + gameObject + "with an animator override controller."); 
			}
			else
			{
				var animatorOverrideController = character.GetAnimatorOverrideController();			
				animator.runtimeAnimatorController = character.GetAnimatorOverrideController();	// el runtimeAnimatorController lo alojas en la variable Animator del scope de la clase.
				animatorOverrideController [DEFAULT_ATTACK] = currentWeaponConfig.GetAttackAnimClip(); 	// access animations list, find the one called Default attack and change ir... override
				

			}
        }

		GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();	// we could have an array of dominant hands
			int numberOfDominantHands = dominantHands.Length;				// get the number of dominant hands in that array.
			
			// handle 0														// UnityEngine Assertions
			// Assert.AreNotEqual (numberOfDominantHands, 0, "not dominat hand found on PlayerController");
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts");

			// si llegamos hasta acá, es porque encontramos un DominantHand
			return dominantHands[0].gameObject;	// agarramos el primer objecto que encontramos en el array dominantHands
		}

        float CalculateDamage()		// Check for additional WeaponConfig damage
        {
			// normal damage
			return baseDamage + currentWeaponConfig.GetAdditionalDamage();
        }




	}
}
