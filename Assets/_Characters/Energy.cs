using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
    public class Energy : MonoBehaviour
	{
		[SerializeField] Image energyOrbBar = null;
		[SerializeField] float maxEnergyPoints = 100f;
		[SerializeField] float regenPointPerSecond = 1f;

		float currentEnergyPoints;

		CameraUI.CameraRaycaster cameraRaycaster = null;

		void Start () 
		{
			SetCurrentMaxEnergy();
		}

		void Update()
		{
			if (currentEnergyPoints < maxEnergyPoints)
			{
				AddEnergyPoints();
				
				UpdateEnergyOrbBar();
			}
		}

        private void AddEnergyPoints()
        {
            var pointsToAdd = regenPointPerSecond * Time.deltaTime;
			currentEnergyPoints = Mathf.Clamp (currentEnergyPoints + pointsToAdd, 0, maxEnergyPoints);
        }

        // Is enough energy available?
        public bool IsEnergyAvailabe (float amount)
		{
			return amount <= currentEnergyPoints;
		}

        public void ConsumeEnergy(float amout)
        {
            float newEnergyPoints = currentEnergyPoints - amout;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, maxEnergyPoints);

			UpdateEnergyOrbBar();
            //currentEnergyPoints = Mathf.Clamp(currentEnergyPoints - pointPerHit, 0, maxEnergyPoints);
        }

        private void UpdateEnergyOrbBar()
        {
            energyOrbBar.fillAmount = 	EnergyAsPercent();
        }

		private void SetCurrentMaxEnergy()
        {
            currentEnergyPoints = maxEnergyPoints;  // Set max points
        }

		float EnergyAsPercent()
		{
			return currentEnergyPoints / maxEnergyPoints;
		}

	}
	
}
