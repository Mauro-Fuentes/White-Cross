using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ChangeLevelManager : MonoBehaviour
{
	public string nextLevel;

	void OnTriggerEnter()
	{
		SceneManager.LoadScene (nextLevel);
	}

}