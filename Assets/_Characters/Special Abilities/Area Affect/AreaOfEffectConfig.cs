using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{

	[CreateAssetMenu(menuName = ("RPG/Special Ability/Area Effect"))]
	public class AreaOfEffectConfig : AbilityConfig
	{
		
		[Header("Area Effect")]
		[SerializeField] float radius = 5f;
		[SerializeField] float damageToEachTarget = 15f;

		public override void AttachComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviourComponent = gameObjectToAttachTo.AddComponent<AreaOfEffectBehaviour>();
			behaviourComponent.SetConfig( this );
			Behaviour = behaviourComponent;
		}

		public float GetDamageToEachTarget()
		{
			return damageToEachTarget;
		}

		public float GetRadius()
		{
			return radius;
		}
	}
}