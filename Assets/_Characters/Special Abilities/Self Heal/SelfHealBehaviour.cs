using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;
using RPG.Core;
using System;

public class SelfHealBehaviour : AbilityBehaviour
{

    Player player = null;

    void Start()
    {
        player = GetComponent<Player>();
    }

	public override void Use(AbilityUseParams useParams)
    {
        RestoreLife (useParams);
		PlayParticleEffect();
        PlayAbilitySound();
    }

    private void RestoreLife (AbilityUseParams useParams)
    {
        player.Heal( (config as SelfHealConfig).GetAmountOfHeal() );

    }
}
