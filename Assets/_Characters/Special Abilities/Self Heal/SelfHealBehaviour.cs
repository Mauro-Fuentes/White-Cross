using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Characters;

public class SelfHealBehaviour : AbilityBehaviour
{

    PlayerController player = null;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

	public override void Use(GameObject target)
    {
        PlayAbilitySound();
        
        var playerHealth = player.GetComponent<HealthSystem>();
        playerHealth.Heal( (config as SelfHealConfig).GetAmountOfHeal() );

		PlayParticleEffect();
        PlayAnimationClip();
    }

}
