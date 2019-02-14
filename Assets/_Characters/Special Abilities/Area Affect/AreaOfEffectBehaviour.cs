using UnityEngine;
using RPG.Characters;


public class AreaOfEffectBehaviour : AbilityBehaviour
{
    // call the main functions from AbilityBehaviour.cs
	public override void Use(GameObject target)
    {
        DealRadialDamage();
		PlayParticleEffect();
        PlayAbilitySound();
    }

    private void DealRadialDamage ()
    {
        // static sphere cast for targets
        RaycastHit[] hits = Physics.SphereCastAll
        (   
            transform.position, 
            (config as AreaOfEffectConfig).GetRadius(), 
            Vector3.up, 
            (config as AreaOfEffectConfig).GetRadius()
        );

        // for every hit I got in array hits
        foreach (RaycastHit hit in hits)
        {
            // check if it's IDamageable
            var damageable = hit.collider.gameObject.GetComponent<HealthSystem>();

            // che if it's the player
            bool hitPlayer = hit.collider.gameObject.GetComponent<PlayerMovement>();
            
            // if it is | and it's NOT the player, AdjustHealth
            if (damageable != null && !hitPlayer)
            {
                float damageToDeal = (config as AreaOfEffectConfig).GetDamageToEachTarget();
                damageable.TakeDamage(damageToDeal);
            }
        }
    }
}
