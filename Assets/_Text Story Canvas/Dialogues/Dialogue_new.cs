using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Dialogue new")]
public class Dialogue_new : ScriptableObject
{
	[TextArea(3, 10)]
	public string[] sentences;

}
