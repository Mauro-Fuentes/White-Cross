using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBridge : MonoBehaviour
{
	[SerializeField] GameObject bridge;
	[SerializeField] AudioClip bridgeUp;

	void OnTriggerStay()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			playSound();
			AnimateBridge();	
		}
	}

    private void playSound()
    {
        bridge.GetComponent<AudioSource>().PlayOneShot(bridgeUp);
    }

    private void AnimateBridge()
    {
	
        bridge.GetComponent<Animator>().SetBool("BridgeUp", true);
    }

}
