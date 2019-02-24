using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
	[SerializeField] AudioClip[] walkingSounds;
	AudioSource audioSource;

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();	

	}


	void PlayWalkSound () 
	{
		var clip = walkingSounds [UnityEngine.Random.Range (0, walkingSounds.Length)];

		audioSource.PlayOneShot(clip);

	}

}
