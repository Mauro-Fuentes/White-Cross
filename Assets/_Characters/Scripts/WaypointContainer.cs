using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{	
	public class WaypointContainer : MonoBehaviour
	{
		public float pointRadious = 1f;
		
		private void OnDrawGizmos()
		{
			Vector3 firstPosition = transform.GetChild(0).position;
			Vector3 previousPosition = firstPosition;

			foreach (Transform waypoint in transform)
			{
				Gizmos.color = new Color (0f, 100f, 255f, .5f); 
				Gizmos.DrawSphere (waypoint.position, pointRadious);
				Gizmos.DrawLine(previousPosition, waypoint.position);
				previousPosition = waypoint.position;
			}
			Gizmos.DrawLine(previousPosition, firstPosition);

		}	
		
	}
}
