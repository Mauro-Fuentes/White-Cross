///////// TEMPLATE FOR CREATING ABILITIES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	// PowewAttackConfig va a agregar un PowerAttackBehaviour
	[CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]
	public class PowerAttackConfig : AbilityConfig
	{
		
		[Header("Power Attack Specific")]
		[SerializeField] float extraDamage = 10f;
		
		
		// supongo que override lo que hace es tomar la clase abstracta de la que hereda y la hace propia.
		public override void AttachComponentTo (GameObject gameObjectToAttachTo)
		{
			// Add a component PowerAttackBehaviour to that gameObject you passed on and stash it into behaviourComponent variable.
			var behaviourComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehaviour>();

			behaviourComponent.SetConfig( this );

			Behaviour = behaviourComponent;
		}

		public float GetExtraDamage()
		{
			return extraDamage;
		}
	}
}