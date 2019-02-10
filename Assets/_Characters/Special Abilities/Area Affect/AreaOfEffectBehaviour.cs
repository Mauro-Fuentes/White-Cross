using UnityEngine;
using RPG.Characters;
using RPG.Core;

public class AreaOfEffectBehaviour : AbilityBehaviour
{
    // call the main functions from AbilityBehaviour.cs
	public override void Use(AbilityUseParams useParams)
    {
        DealRadialDamage (useParams);
		PlayParticleEffect();
        PlayAbilitySound();
    }

    private void DealRadialDamage (AbilityUseParams useParams)
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
            var damageable = hit.collider.gameObject.GetComponent<IDamageable>();

            // che if it's the player
            bool hitPlayer = hit.collider.gameObject.GetComponent<Player>();
            
            // if it is | and it's NOT the player, AdjustHealth
            if (damageable != null && !hitPlayer)
            {
                float damageToDeal = useParams.baseDamage + (config as AreaOfEffectConfig).GetDamageToEachTarget(); //TODO:
                damageable.TakeDamage(damageToDeal);
            }
        }
    }
}
