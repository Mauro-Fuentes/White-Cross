using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
	[SerializeField] Text textComponent;	// UI text Object in canvas
	[SerializeField] State startingState;

	// Talk to State (scriptableObject)
	State state;
	
	void Start () 
	{
		state = startingState;
		textComponent.text = state.GetStateStory();

	}


	void Update () 
	{
		ManageState();
	}

    private void ManageState()
    {
        State[] nextState = state.GetNextState();
		
		if (Input.GetKeyDown (KeyCode.Return))
		{
			state = nextState[1];
		}
    }
}
