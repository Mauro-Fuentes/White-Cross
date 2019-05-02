using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

public class OpenChest : MonoBehaviour
{
    TextMesh textMesh;
    Animator animator;
    Camera startCamera;

    [SerializeField] AudioClip[] chestSounds;

    // If Player enters
    // public delegate void OnPlayerEnter (Character player); 		
    // public event OnPlayerEnter onPlayerEnter;

    bool alreadyCollected = false;

    void OnTriggerEnter(Collider col)
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            var player = col.GetComponent<Character>();

            textMesh = GetComponentInChildren<TextMesh>();
            textMesh.text = "Open me. Press E";

            startCamera = Camera.main;
   
            textMesh.transform.LookAt(startCamera.transform);

            // // Call the Delegate
            // onPlayerEnter(player);
  
            if (Input.GetKeyDown(KeyCode.E) && alreadyCollected == false)
            {
                alreadyCollected = true;
                
                var audioSource = GetComponent<AudioSource>();
                
                var clip = chestSounds [UnityEngine.Random.Range (0, chestSounds.Length)];
		        audioSource.PlayOneShot(clip);

                animator = GetComponent<Animator>();
                animator.SetBool ("Open", true);
            }
        }
    }

    
}
