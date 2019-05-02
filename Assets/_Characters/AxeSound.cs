using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSound : MonoBehaviour
{
	[SerializeField] AudioClip[] axeSounds;
	AudioSource audioSource;

	[SerializeField] Transform particles1;
	[SerializeField] Transform place;

	void PlaySound() 
	{
		audioSource = GetComponent<AudioSource>();
		var clip = axeSounds [UnityEngine.Random.Range (0, axeSounds.Length)];
		audioSource.PlayOneShot(clip);
	}

	void SpawnAttackParticle()
	{
		// particles1 = GetComponent<Transform>();
		// var playAttackParticle = Instantiate ( particles1, transform.position, particles1.transform.rotation);

		var playAttackParticle = Instantiate ( particles1, place.transform.position, particles1.transform.rotation);
	}
}

