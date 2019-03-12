using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
	public Camera startCamera;
	public Camera secondCamera;

	bool startCameraOn = true;

	void start () 
	{

	}

	void OnTriggerEnter () 
	{

		CameraSwitch();
	}

    void CameraSwitch()
    {
        if (startCameraOn)
		{
			secondCamera.enabled = Camera.main;
			startCamera.enabled = false;
		}
		startCameraOn = !startCameraOn;
    }
}
