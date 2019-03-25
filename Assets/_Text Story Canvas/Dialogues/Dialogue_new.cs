using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Dialogue new")]		// As it's not a mono we need this in order to see it on the inspector.
public class Dialogue_new : ScriptableObject
{
	[TextArea(3, 10)]
	public string[] sentences;

}
