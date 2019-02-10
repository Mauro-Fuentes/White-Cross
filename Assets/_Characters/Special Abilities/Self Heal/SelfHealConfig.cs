using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{

	[CreateAssetMenu(menuName = ("RPG/Special Ability/Self Heal"))]
	public class SelfHealConfig : AbilityConfig
	{

		[Header("Self Heal Specific")]
		[SerializeField] float amountOfHeal = 55f;
		

		public override void AttachComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviourComponent = gameObjectToAttachTo.AddComponent<SelfHealBehaviour>();

			behaviourComponent.SetConfig( this );

			Behaviour = behaviourComponent;
		}

		public float GetAmountOfHeal()
		{
			return amountOfHeal;
		}

	}
}