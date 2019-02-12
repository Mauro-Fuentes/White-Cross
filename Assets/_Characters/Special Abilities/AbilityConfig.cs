﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Characters
{
	public struct AbilityUseParams
	{
		public IDamageable target;
		public float baseDamage;

		// Constructor
		public AbilityUseParams (IDamageable target, float baseDamage)
		{
			this.target = target;
			this.baseDamage = baseDamage;
		}
	}

	public abstract class AbilityConfig : ScriptableObject		// Abstract porque no vamos a hacer ninguna instacia de esta clase.
	{
		[Header("Special Ability General")]
		[SerializeField] float energyCost = 10f;
		[SerializeField] GameObject particlePrefab = null;
		[SerializeField] AudioClip[] audioClips = null;

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

		// Implementation of Use() from interfase ISpecialAbility
		public void Use(AbilityUseParams useParams)
		{
			behaviour.Use(useParams);
		}

		public AudioClip GetRandomAbilitySound()
		{
			return audioClips[Random.Range(0, audioClips.Length)];
		}
	}
	


}