using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour 
{
	private GameObject child;
	[SerializeField] AudioClip pauseSoundIn;
	[SerializeField] AudioClip pauseSoundOFF;

	public Animator animator;

	bool isInPause;
	const int WAIT_TWO_SECONDS = 2;

	public void GoToMainMenu()
	{
		SceneManager.LoadScene (0);
	}

	void Update () 
	{
		FreezeTime();
	}

	void FreezeTime ()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{

			StartCoroutine (PlaySound());

			if (!isInPause)
			{ 
				StartCoroutine (WaitUntilAnimIsDone());
				isInPause = !isInPause;
			}
			else
			{
				DeactiveFreeze();
			}
		}
        
	}

    IEnumerator WaitUntilAnimIsDone()
	{
		animator.SetBool ("IN", true);
		yield return new WaitForEndOfFrame();
	}

	IEnumerator PlaySound()
	{
		var audiosource = gameObject.GetComponent<AudioSource>();
		audiosource.PlayOneShot(pauseSoundIn);
		yield return null;
	}

	public void DeactiveFreeze()	// Método extraído de FreezeTime()... else;
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(pauseSoundOFF);

		animator.SetBool ("IN", false);
		Time.timeScale = 1f;
		isInPause = !isInPause;

	}

  public void QuitGame ()
	{	
		Application.Quit();
	}

}
