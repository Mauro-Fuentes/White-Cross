using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public Transform target;
    bool hasEntered;
    void Start()
    {
        StartCoroutine(AutoDestroyObject());
    }

    IEnumerator AutoDestroyObject()
    {
        var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget < 5)
        {
            hasEntered = true;
        }

        if (hasEntered)
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
            yield return null;
        }
    }
}
