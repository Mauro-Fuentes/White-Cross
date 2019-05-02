using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineController : MonoBehaviour
{
	public List<PlayableDirector> playableDirectors;

	//public SceneFades sceneFadesAccess;

	public void OnTriggerEnter()
	{
		foreach (PlayableDirector playableDirector in playableDirectors)
		{
			playableDirector.Play();
		}
		
	}

	void OnEnable()
	{
		//Debug.Log ("STOP PLAYING");
		//sceneFadesAccess = GetComponent<SceneFades>();
		//sceneFadesAccess.FadeCanvasGroup();

		var a = GetComponent<ChangeLevelManager>();
		a.LoadNextScene();
	}
}
