using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = ("RPG/Weapon"))]
	public class Weapon : ScriptableObject
	{
		public Transform gripTransform;						// quiero dejarlo publico para que todos puedan cambiarlo
		
		[SerializeField] GameObject weaponPrefab;			// connect the GameObject.
		[SerializeField] AnimationClip attackAnimation;		// animation attack
		[SerializeField] float minTimeBetweenHits = 0.5f;
		[SerializeField] float maxAttackRange = 2f;
		[SerializeField] float additionalDamage = 10f;

		public float GetMinTimeBetweenHits()
		{
			return minTimeBetweenHits;
		}
		public float GetMaxAttackRange()
		{
			return maxAttackRange;
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