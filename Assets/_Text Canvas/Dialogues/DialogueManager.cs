using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	// FIFO collection
	Queue<string> sentences;

	public Text dialogueText;

	public Animator animator;

	void Start () 
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue_new dialogue)
	{
		animator.SetBool ("DialogueIsIN", true);

		sentences.Clear();

		foreach (string sentece in dialogue.sentences)
		{
			sentences.Enqueue(sentece);
		}

		DisplayNextSentece();

	}

	public void DisplayNextSentece()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentece = sentences.Dequeue();

		StopAllCoroutines();
		
		StartCoroutine (TypeSentence(sentece));

	}

	IEnumerator TypeSentence (string sentece)
	{
		dialogueText.text = "";
		foreach (char letter in sentece.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool ("DialogueIsIN", false);
	}

}
