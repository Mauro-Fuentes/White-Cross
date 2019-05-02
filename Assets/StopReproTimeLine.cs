using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopReproTimeLine : MonoBehaviour
{

    public UnityEvent onSpacebar;
    //public UnityEvent onReturn;

    public void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onSpacebar != null)
            {
                onSpacebar.Invoke();
            }
        }

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if (onReturn != null)
        //    {
        //        onReturn.Invoke();
        //    }
        //}
    }
}

