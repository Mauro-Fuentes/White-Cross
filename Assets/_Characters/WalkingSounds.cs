using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
	[SerializeField] AudioClip[] walkingSounds;
	AudioSource audioSource1;

	void PlayWalkSound () 
	{
		audioSource1 = GetComponent<AudioSource>();
		var clip = walkingSounds [UnityEngine.Random.Range (0, walkingSounds.Length)];

		audioSource1.PlayOneShot(clip);

	}

}
