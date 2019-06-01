using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LastLevel : MonoBehaviour
{
    public string nextLevel;

    void OnEnable()
    {
        SceneManager.LoadScene(nextLevel);
    }

}