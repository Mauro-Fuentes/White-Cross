using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace RPG.Characters
{	
	public class HealthSystem : MonoBehaviour
	{
		
		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] Image healthBar;

		[SerializeField] AudioClip[] damageSounds;
		[SerializeField] AudioClip[] deathSounds;

		//[SerializeField] float deathVanishSeconds = 1;

		public GameObject particlePrefab;

		const string DEATH_TRIGGER = "Die";
		const float PARTICLE_CLEAN_UP_DELAY = 5f;

		float currentHealthPoints = 0;

		Animator animator;	
		AudioSource audioSource = null;
		Character characterMovement;

        bool characterIsDead;

        public UnityEvent tellMeTheBossDiedEvent;

        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

		void Start () 
		{
			animator = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
			characterMovement = GetComponent<Character>();
			

			currentHealthPoints = maxHealthPoints;
		}

		void Update () 
		{
			UpdateHealthBar();
		}

        void UpdateHealthBar()
        {
            if (healthBar) // may not have health bars to update
			{
				healthBar.fillAmount = healthAsPercentage;
			}
		}

		public void TakeDamage(float damage)
		{	

			// Reduce health
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			
			// get random clip
			var clip = damageSounds[UnityEngine.Random.Range (0, damageSounds.Length)];

			audioSource.PlayOneShot(clip);

			if (currentHealthPoints <= 0)
			{
				StartCoroutine(KillCharacter());
                tellMeTheBossDiedEvent.Invoke(); // Invoke UnityEvent
            }
			
		}

		public void Heal (float amount)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0f, maxHealthPoints);
		}

		IEnumerator KillCharacter()
		{
			characterMovement.Kill();
			
			animator.SetTrigger (DEATH_TRIGGER);

			audioSource.clip = deathSounds [UnityEngine.Random.Range(0, deathSounds.Length)];
			audioSource.Play();
			yield return new WaitForSecondsRealtime (audioSource.clip.length);

			// reset the transform of particlePrefab, otherwise it doesn't work
			particlePrefab.transform.position = new Vector3(0,0,0);
			
			var playDeathParticle = Instantiate ( particlePrefab, transform.position, particlePrefab.transform.rotation);
			
			playDeathParticle.transform.parent = transform;
			
			playDeathParticle.GetComponent<ParticleSystem>().Play();

			StartCoroutine (DestroyParticle(playDeathParticle));

			yield return new WaitForSecondsRealtime (audioSource.clip.length);
		
			var playerComponent = GetComponent<PlayerController>();

			if (playerComponent && playerComponent.isActiveAndEnabled) // relying on lazy evaluation
			{
				yield return new WaitForSeconds(4); //TODO: take out magic number
				SceneManager.LoadScene (0); // TODO:
			}
			else
			{
				//Destroy(gameObject, deathVanishSeconds);
			}

		}

		IEnumerator DestroyParticle(GameObject playDeathParticle)
		{
			yield return new WaitForSeconds (PARTICLE_CLEAN_UP_DELAY);
			Destroy(playDeathParticle);
			yield return new WaitForEndOfFrame();
		}

    }
}
