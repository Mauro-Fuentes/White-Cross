using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Characters;

public class TriggerDeathEvent : MonoBehaviour
{
    public GameObject achievements;

    private void Start()
    {
        achievements = GetComponent<GameObject>();
    }
    public void MessageOne()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5f);

       
        achievements.SetActive(false);
    }
}
