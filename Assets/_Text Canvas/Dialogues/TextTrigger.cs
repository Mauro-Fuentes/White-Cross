using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] Dialogue_new dialogue; // creates a slot for the non mono Dialogue
	[SerializeField] float playerDistanceThreshold = 1f;

	bool hasPlayed = false;

	GameObject player; // will only 

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDistanceThreshold)
        {
            RequestTriggerDialogue();
			
        }
    }

    void RequestTriggerDialogue()
    {
        if (hasPlayed)
        {
            return;
        }
        else
        {
            StartCoroutine (TriggerDialogue());
            hasPlayed = true;
        }
    }

    IEnumerator TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return null;
	}

	void OnDrawGizmos()
    {
        Gizmos.color = new Color(100f, 255f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, playerDistanceThreshold);
    }

}
