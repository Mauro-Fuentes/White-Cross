using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RPG.Characters
{	
	public class HealthSystem : MonoBehaviour
	{

		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] Image healthBar;

		[SerializeField] AudioClip[] damageSounds;
		[SerializeField] AudioClip[] deathSounds;

		[SerializeField] float deathVanishSeconds = 2;

		const string DEATH_TRIGGER = "Die";

		float currentHealthPoints = 0;

		Animator animator;
				
		AudioSource audioSource = null;

		CharacterMovement characterMovement;

		public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }



		void Start () 
		{
			animator = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
			characterMovement = GetComponent<CharacterMovement>();

			currentHealthPoints = maxHealthPoints;
		}

		void Update () 
		{
			UpdateHealthBar();
		}

        void UpdateHealthBar()
        {
            if (healthBar) // may not habe health bars to update
			{
				healthBar.fillAmount = healthAsPercentage;
			}
		}

		public void TakeDamage(float damage)
		{	
			bool characterDies = (currentHealthPoints - damage <= 0);

			// Reduce health
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			
			// get random clip
			var clip = damageSounds[UnityEngine.Random.Range (0, damageSounds.Length)];

			audioSource.PlayOneShot(clip);

			if (currentHealthPoints <= 0)
			{
				StartCoroutine(KillCharacter());
			}
			
		}

		IEnumerator KillCharacter()
		{
			StopAllCoroutines();

			characterMovement.Kill();
			
			animator.SetTrigger (DEATH_TRIGGER);

			var playerComponent = GetComponent<Player>();
			if (playerComponent && playerComponent.isActiveAndEnabled) // relying on lazy evaluation
			{
				audioSource.clip = deathSounds [UnityEngine.Random.Range(0, deathSounds.Length)];
			}

			audioSource.clip = deathSounds[UnityEngine.Random.Range (0, deathSounds.Length)];
			
			// TODO: check for this audio
			if (audioSource.isPlaying)
			{
				yield return new WaitForSecondsRealtime (audioSource.clip.length);
			}
			else
			{
				audioSource.Play();
				Destroy(gameObject, deathVanishSeconds);
			}

			yield return new WaitForSecondsRealtime (audioSource.clip.length);
			
			yield return new WaitForSecondsRealtime (deathVanishSeconds);


			SceneManager.LoadScene (0);
		}

		public void Heal (float amount)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0f, maxHealthPoints);
		}
    }
}
