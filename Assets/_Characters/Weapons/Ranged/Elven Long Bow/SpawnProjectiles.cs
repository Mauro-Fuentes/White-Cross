using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
	public Transform firePoint;

	public List<GameObject> listOfVfx = new List<GameObject> ();
	private GameObject effectToSpawn;
	GameObject vfx;
	public GameObject target;

	void Start () 
	{
		effectToSpawn = listOfVfx [0];
	}

	void Update () 
	{
		if (Input.GetMouseButton (0))
		{
			UpdateRotatio();
			SpawVFX();
		}
	}

	void SpawVFX()
	{
		if (firePoint != null)
		{
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, firePoint.transform.rotation);

		}
	}

	void UpdateRotatio()
	{
		transform.LookAt(target.transform);
	}


}
