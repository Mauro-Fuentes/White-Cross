using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
	public class CameraFollow : MonoBehaviour
	{
		GameObject player;

		void Start () 
		{
			// Static funcion of Unity
			player = GameObject.FindGameObjectWithTag("Player");

		}

		void LateUpdate()
		{
			transform.position = player.transform.position;
		}

	}
}
