using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
	[SerializeField] GameObject doorObject;
	[SerializeField] AudioClip doorUpSound;
	
	void OnTriggerStay()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			AnimateDoor();
			playSound();
		}

	}

    private void playSound()
    {
		GetComponent<AudioSource>().PlayOneShot(doorUpSound);
    }

    private void AnimateDoor()
    {
        doorObject.GetComponent<Animator>().Play("Open door");
    }
}
