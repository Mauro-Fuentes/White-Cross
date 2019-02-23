using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class AbilityConfig : ScriptableObject		// Abstract porque no vamos a hacer ninguna instacia de esta clase.
	{
		[Header("Special Ability General")]
		[SerializeField] float energyCost = 10f;
		[SerializeField] GameObject particlePrefab = null;
		[SerializeField] AudioClip[] audioClips = null;
		[SerializeField] AnimationClip animationClip = null;

		// only children can set this property
		protected AbilityBehaviour behaviour;

		public abstract AbilityBehaviour GetBehaviourComponent (GameObject objectToAttachTo);

		public void AttachAbilityTo (GameObject objectToAttachTo)
		{
			AbilityBehaviour behaviourComponent = GetBehaviourComponent (objectToAttachTo);
			behaviourComponent.SetConfig(this);
			behaviour = behaviourComponent;
		} 
		
	
		// Getter for the private particlePrefab
		public GameObject GetParticlePrefab()
		{
			return particlePrefab;
		}

		// Getter for the private energyCost
		public float GetEnergyCost()
		{
			return energyCost;
		}

		// Getter
		public AudioClip GetRandomAbilitySound()
		{
			return audioClips[Random.Range(0, audioClips.Length)];
		}

		// Getter
		public AnimationClip GetAnimationClip()
		{
			return animationClip;
		}

		public void Use(GameObject target)
		{
			behaviour.Use(target);
		}

	}
	
}