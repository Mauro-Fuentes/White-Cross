using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
    public class SpecialAbilities : MonoBehaviour
	{
		// Temporarily serialized for debug
		[SerializeField] AbilityConfig [] abilities;
		
		[SerializeField] Image GreenOrbBar;
		[SerializeField] float maxEnergyPoints = 100f;
		[SerializeField] float regenPointPerSecond = 1f;
		[SerializeField] AudioClip outOfEnergy;

		float currentEnergyPoints;

		AudioSource audioSource;

		float energyAsPercent { get { return currentEnergyPoints / maxEnergyPoints; } }

		void Start () 
		{
			audioSource = GetComponent<AudioSource>();

			SetCurrentMaxEnergy();
			AttachInitialAbilities();
			UpdateEnergyOrbBar();

		}

		void Update()
		{
			if (currentEnergyPoints < maxEnergyPoints)
			{
				AddEnergyPoints();
				UpdateEnergyOrbBar();
			}
		}

		void AttachInitialAbilities()
        {
			// start array from 0; the length of the array is shorter than ability index; increment by 1.
            for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex ++)
			{
				// Add a component type SpecialAbilityConfig... pass this very same gameobject
				abilities[abilityIndex].AttachAbilityTo (gameObject);
				// this checks what ability the player has and add a CS file behaviour.
			}
			
        }

		public void AttemptSpecialAbility(int abilityIndex, GameObject target = null)
        {
            var energyComponent = gameObject.GetComponent<SpecialAbilities>();
			
			var energyCost = abilities[abilityIndex].GetEnergyCost();

			if (energyCost <= currentEnergyPoints)
			{
				ConsumeEnergy (energyCost);
				abilities[abilityIndex].Use(target);
			}
			else
			{
				if (!audioSource.isPlaying)				
				{
					audioSource.PlayOneShot(outOfEnergy);
				}
			}
        }

		public int GetNumberOfAbilities()
		{
			return abilities.Length;
		}

        private void AddEnergyPoints()
        {
            var pointsToAdd = regenPointPerSecond * Time.deltaTime;
			currentEnergyPoints = Mathf.Clamp (currentEnergyPoints + pointsToAdd, 0, maxEnergyPoints);
        }

        public void ConsumeEnergy(float amout)
        {
            float newEnergyPoints = currentEnergyPoints - amout;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, maxEnergyPoints);

			UpdateEnergyOrbBar();
        }

        private void UpdateEnergyOrbBar()
        {
            GreenOrbBar.fillAmount = 	energyAsPercent;
        }

		private void SetCurrentMaxEnergy()
        {
            currentEnergyPoints = maxEnergyPoints;  // Set max points
        }

	}
	
}
