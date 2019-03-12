using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingletton : MonoBehaviour
{
	////////////////////////// SINGLETON CAMERA

	public static CameraSingletton cameraToLookAt {get; private set;}            ////////////


	private void Awake() // para que cargue antes que Start()
	{
		// Setting the Singleton. A singleton its actually a just a good practice.

		if (cameraToLookAt == null)				// Si el tipo MyGameManager cameraToLookAt no está
		{	// Tambien significa que el código está corriendo por primera vez.

			cameraToLookAt = this;				// Hacer una instancia this (MyGameManager)
			DontDestroyOnLoad (gameObject);	// Este gameObject
		}
		else
		{
			Destroy(gameObject);	
		}
	}

//////////////////////////////////
}
