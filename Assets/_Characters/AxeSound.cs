using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSound : MonoBehaviour
{
	[SerializeField] AudioClip[] axeSounds;
	AudioSource audioSource;

	void PlaySound() 
	{
		audioSource = GetComponent<AudioSource>();
		var clip = axeSounds [UnityEngine.Random.Range (0, axeSounds.Length)];
		audioSource.PlayOneShot(clip);
	}

}

