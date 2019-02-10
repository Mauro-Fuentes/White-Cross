using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

 // TODO: decoupling 
using RPG.CameraUI;
using RPG.Core; 
using RPG.Weapons;

namespace RPG.Characters
{
	public class Player : MonoBehaviour, IDamageable
	{
 
		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float baseDamage = 10f;
		
		[Range (.1f, 1.0f)] [SerializeField] float criticalHitChange = 0.1f;
		[SerializeField] float criticalHitMultiplier = 1.25f;

		[SerializeField] AnimatorOverrideController AnimatorOverrideController = null;
		
		[SerializeField] Weapon weaponInUse = null;	// tipo ScriptableObject

		[Space(10)]	// Temporarily serialized for debug
		[SerializeField] AbilityConfig [] abilities;

		[SerializeField] ParticleSystem criticalHitParticles = null;

		float currentHealthPoints = 0;
		float lastHitTime = 0;

		public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }
		
		AudioSource audioSource = null;
		Enemy enemy = null;
		Animator animator = null;
		CameraRaycaster cameraRaycaster = null;

		
		[Header("Damage Volume")]
		const float maxVolume = 1f;
		const float minVolume = 0.01f;
		[Space(10)]
		[Range(minVolume, maxVolume)]
		public float volume;
		[SerializeField] AudioClip[] damageSounds;
		[Space(10)]
		[SerializeField] AudioClip[] deadSounds;

		const string DEATH_TRIGGER = "Die";
		const string ATTACK_TRIGGER = "Attack";



		void Start ()
        {
            audioSource = GetComponent<AudioSource>();

			RegisterForMouseClick();
            
			SetCurrentMaxHealth();
            
			PutWeaponInHand();
            
			SetupRuntimeAnimator();
			
			AttachInitialAbilities();
			
        }

        private void AttachInitialAbilities()
        {
			// start array from 0; the length of the array is shorter than ability index; increment by 1.
            for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex ++)
			{
				// Add a component type SpecialAbilityConfig... pass this very same gameobject
				abilities[abilityIndex].AttachComponentTo (gameObject);
				// this checks what ability the player has and add a CS file behaviour.
			}
			
        }

        void Update()
		{
			// if player is alive
			if (healthAsPercentage > Mathf.Epsilon)
			{
				ScanForAbilityKeyDown();
			}
		}
		
		private void ScanForAbilityKeyDown()
		{
			// keyIndex starts at 1, keyIndex is never greater than abilities array, increment 1
			for (int keyIndex = 1; keyIndex < abilities.Length; keyIndex ++)
			{
				if (Input.GetKeyDown(keyIndex.ToString()))
				{
					// attempt the abillity according to the keyIndex 
					AttemptSpecialAbility(keyIndex);
				}
			}
		}

		public void TakeDamage(float damage)
		{	
		
			// Reduce health
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			audioSource.Play();
			
			audioSource.clip = damageSounds[UnityEngine.Random.Range (0, damageSounds.Length)];
			audioSource.volume = volume;

			if (currentHealthPoints <= 0)
			{
				StartCoroutine(KillPlayer());
			}
			
		}

		public void Heal (float amount)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0f, maxHealthPoints);
		}

		IEnumerator KillPlayer()
		{
			animator.SetTrigger (DEATH_TRIGGER);

			audioSource.clip = deadSounds[UnityEngine.Random.Range (0, deadSounds.Length)];
			if (audioSource.isPlaying)
			{
				yield return new WaitForSecondsRealtime (audioSource.clip.length);
			}
			else
			{
				audioSource.volume = 0.7f;
				audioSource.Play();
			}

			yield return new WaitForSecondsRealtime (audioSource.clip.length);
			
			yield return new WaitForSecondsRealtime (2f);

			SceneManager.LoadScene (0);
		}

        private void SetCurrentMaxHealth()
        {
            currentHealthPoints = maxHealthPoints;  // Set max points
        }

        private void SetupRuntimeAnimator()
        {
			animator = GetComponent<Animator>();							// fetch the AnimatorOverrideController and store it in animator
			
			animator.runtimeAnimatorController = AnimatorOverrideController;	// el runtimeAnimatorController lo alojas en la variable Animator del scope de la clase.

			AnimatorOverrideController ["DEFAULT ATTACK"] = weaponInUse.GetAttackAnimClip(); 	// access animations list, finde the one called Default attack and change ir... override
        }

        private void PutWeaponInHand()
		{
			var weaponPrefab = weaponInUse.GetWeaponPrefab();
			GameObject dominantHand = RequestDominantHand();

			var weapon = Instantiate (weaponPrefab, dominantHand.transform);
			weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
			weapon.transform.localRotation= weaponInUse.gripTransform.localRotation;
		}

		private GameObject RequestDominantHand()
		{
			var dominantHands = GetComponentsInChildren<DominantHand>();	// we could have an array of dominant hands
			int numberOfDominantHands = dominantHands.Length;				// get the number of dominant hands in that array.
			
			// handle 0														// UnityEngine Assertions
			// Assert.AreNotEqual (numberOfDominantHands, 0, "not dominat hand found on player");
			Assert.IsFalse (numberOfDominantHands <= 0, "No DominantHand found");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple DominantHand Scripts");

			// si llegamos hasta acá, es porque encontramos un DominantHand
			return dominantHands[0].gameObject;	// agarramos el primer objecto que encontramos en el array dominantHands
		}

		private void RegisterForMouseClick()
		{
			cameraRaycaster = FindObjectOfType<CameraRaycaster>();

			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
		}

		void OnMouseOverEnemy(Enemy enemyToSet)
		{
			// set current enemy
			this.enemy = enemyToSet;

			if (Input.GetMouseButton(0) && IsTargetInRange (enemy.gameObject))
			{
				AttackTarget();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				AttemptSpecialAbility( 0);
			}
		}

        private void AttemptSpecialAbility(int abilityIndex)
        {
            var energyComponent = GetComponent<Energy>();
			var energyCost = abilities[abilityIndex].GetEnergyCost();

			if (energyComponent.IsEnergyAvailabe (energyCost))
			{
				energyComponent.ConsumeEnergy (energyCost);
				
				var abilityParams = new AbilityUseParams (enemy, baseDamage);

				// Take the ability passed, Use it... acording to SpecialAbilityConfig SO
				abilities[abilityIndex].Use(abilityParams);
			}
        }

        private void AttackTarget()
        {
			if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
            {
                animator.SetTrigger(ATTACK_TRIGGER);
                enemy.TakeDamage ( CalculateDamage() );
                lastHitTime = Time.time;
            }
        }

		// Check for additional weapon damage
        private float CalculateDamage()
        {
			// if random number is less or equal than criticalChange then we have a critical hit.
			bool isCriticalHit = UnityEngine.Random.Range (0f, 1f) <= criticalHitChange;
			
			// normal damage
			float damageBeforeCritical = baseDamage + weaponInUse.GetAdditionalDamage();

			// #endregion
			if (isCriticalHit)
			{
				criticalHitParticles.Play();
				return damageBeforeCritical * criticalHitMultiplier;
			}
			else
			{
				return damageBeforeCritical;
			}

        }

        private bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
			return distanceToTarget <= weaponInUse.GetMaxAttackRange();
        }

	}
}