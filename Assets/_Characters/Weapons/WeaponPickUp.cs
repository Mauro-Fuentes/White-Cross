using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

namespace RPG.Weapons
{
	[ExecuteInEditMode]
	public class WeaponPickUp : MonoBehaviour
	{

		[SerializeField] WeaponConfig weaponConfig;
		[SerializeField] AudioClip pickWeaponSFX;

		AudioSource audioSource;

		void Start () 
		{
			audioSource = GetComponent<AudioSource>();
		}

		void Update () 
		{
			// if we are NOT in playing mode
			if (!Application.isPlaying)
			{
				DestroyChildren();
				InstantiateWeapon();
			}
			
		}

        void DestroyChildren()
        {
            foreach (Transform child in transform)
			{
				DestroyImmediate(child.gameObject);
			}
	
        }

        void InstantiateWeapon()
        {
            var WeaponSystem = weaponConfig.GetWeaponPrefab();
			WeaponSystem.transform.position = Vector3.zero;
			Instantiate(WeaponSystem, gameObject.transform);
        }

		void OnTriggerStay()
		{
			FindObjectOfType<PlayerController>().GetComponent<WeaponSystem>().PutWeaponInHand(weaponConfig);
			audioSource.PlayOneShot(pickWeaponSFX);
		}
    }
}

