using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/WeaponConfig"))]
	public class WeaponConfig : ScriptableObject
	{
		public Transform gripTransform;						// quiero dejarlo publico para que todos puedan cambiarlo
		
		[SerializeField] GameObject weaponPrefab;			// connect the GameObject.
		[SerializeField] AnimationClip attackAnimation;		// animation attack
		[SerializeField] float timeBetweenAnimationCycles = 0.5f;
		[SerializeField] float maxAttackRange = 2f;
		[SerializeField] float additionalDamage = 10f;
		[SerializeField] float damageDelay = 0.5f;

		public float GetTimeBetweenCycles()
		{
			return timeBetweenAnimationCycles;
		}
		public float GetMaxAttackRange()
		{
			return maxAttackRange;
		}

		public float GetDamageDelay()
		{
			return damageDelay;
		}

		public GameObject GetWeaponPrefab()		//método para obtener el gameobject.
		{
			return weaponPrefab;
		}

		public AnimationClip GetAttackAnimClip()
        {
            RemoveAnimationEvents();
            return attackAnimation;
        }

		public float GetAdditionalDamage()
		{
			return additionalDamage; 
		}
		
		// so that asset pack cannot cause crashes
        private void RemoveAnimationEvents()
        {
            attackAnimation.events = new AnimationEvent[0]; // Este es un método que tiene AnimationClip simplemente hago una lista vacia
            // para que no use ningún evento sin que yo lo sepa.
        }
    }
}