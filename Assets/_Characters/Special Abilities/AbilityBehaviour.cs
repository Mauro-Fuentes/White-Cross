using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class AbilityBehaviour : MonoBehaviour
	{

		protected AbilityConfig config;

		const string ATTACK_TRIGGER = "Attack";
		const string DEFAULT_ATTACK_STATE = "DEFAULT ATTACK";
		const float PARTICLE_CLEAN_UP_DELAY = 10f;

		public abstract void Use(GameObject target = null);

		// Getter
		public void SetConfig (AbilityConfig configToSet)
		{
			config = configToSet;
		}

		protected void PlayParticleEffect()
		{
			var particlePrefab = config.GetParticlePrefab();

			// Remember, this config.GetParticlePrefab comes from the SuperClass AreaOfEffectConfig config in this Scope.
			var particleObject = Instantiate 
			(
				particlePrefab, 
				transform.position, 
				particlePrefab.transform.rotation
			);
			
			particleObject.transform.parent = transform; // check if prebaf requires world space.

			// Now that particleObject has a gameObject !! we need to look up for the actual ParticleSystem
			particleObject.GetComponent<ParticleSystem>().Play();

			StartCoroutine (DestroyParticleWhenFinished(particleObject));
		}

		IEnumerator DestroyParticleWhenFinished(GameObject particlePrefab)
		{
			while (particlePrefab.GetComponent<ParticleSystem>().isPlaying)
			{
				yield return new WaitForSeconds (PARTICLE_CLEAN_UP_DELAY);
			}
			Destroy(particlePrefab);
			yield return new WaitForEndOfFrame();
		}

		// let children know about this
		protected void PlayAbilitySound ()
		{
			var abilitySound = config.GetRandomAbilitySound(); // todo:
			var audioSource = GetComponent<AudioSource>();
			
			// sound will play in top of eachother
			audioSource.PlayOneShot(abilitySound);
		}

		protected void PlayAnimationClip()
		{
			var animatorOverrideController = GetComponent<Character>().GetAnimatorOverrideController();

			var animator = GetComponent<Animator>();
			
			// Let's make them talk to each other
			animator.runtimeAnimatorController = animatorOverrideController; // in runtime use the animatorOverrideController we just find 
			animatorOverrideController [DEFAULT_ATTACK_STATE] = config.GetAnimationClip();	//go to AbilityConfig.cs and GetAnimationClip()
			animator.SetTrigger (ATTACK_TRIGGER);
		}
	}
}