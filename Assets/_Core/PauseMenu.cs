using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
	private GameObject child;
	[SerializeField] AudioClip pauseSoundIn;
	[SerializeField] AudioClip pauseSoundOFF;

	public Animator animator;

	bool isInPause;
	const int WAIT_TWO_SECONDS = 2;

	void Update () 
	{
		FreezeTime();
	}

	void FreezeTime ()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			gameObject.GetComponent<AudioSource>().PlayOneShot(pauseSoundIn);
			
			if (!isInPause)
			{ 
				StartCoroutine (WaitUntilAnimIsDone());
				
				AudioListener.pause = true;
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
		if(!isInPause)
		{
			animator.SetBool ("IN", true);
			yield return new WaitForSecondsRealtime(WAIT_TWO_SECONDS);
			Time.timeScale = 0f;
		}
	}

	public void DeactiveFreeze()	// Método extraído de FreezeTime()... else;
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(pauseSoundOFF);

		animator.SetBool ("IN", false);
		Time.timeScale = 1f;
		AudioListener.pause = false;
		isInPause = !isInPause;

	}

  public void QuitGame ()
	{	
		Application.Quit();
	}

}
