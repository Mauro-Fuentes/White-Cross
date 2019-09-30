using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]		// As it's not a mono we need this in order to see it on the inspector.
public class Dialogue
{
	[TextArea(3, 10)]
	public string[] sentences;

}
