using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
	public Transform firePoint;
	public List<GameObject> listOfVfx = new List<GameObject> ();
	private GameObject effectToSpawn;
	GameObject vfx;

	void Start () 
	{
		effectToSpawn = listOfVfx [0];
	}

	void Update () 
	{
		if (Input.GetMouseButton (0))
		{
			SpawVFX();
		}
	}

	void SpawVFX()
	{
		if (firePoint != null)
		{
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);

		}
	}


}
