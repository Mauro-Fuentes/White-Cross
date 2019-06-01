using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineController : MonoBehaviour
{
	public List<PlayableDirector> playableDirectors;

	public void OnTriggerEnter()
	{
        if (playableDirectors.Count == 0)
        {
            Debug.Log("Error");
        }

        else
        {
            foreach (PlayableDirector playableDirector in playableDirectors)
            {
                playableDirector.Play();
            }
        }

		
	}


}
