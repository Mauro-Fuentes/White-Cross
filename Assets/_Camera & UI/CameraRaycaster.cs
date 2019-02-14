using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using RPG.Characters; 

namespace RPG.CameraUI
{
    public class CameraRaycaster : MonoBehaviour
	{

		[SerializeField] Texture2D walkCursor = null;
		[SerializeField] Texture2D enemyCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2 (0, 0);	// El tamaño de los iconos que importe		

		const int POTENTIALLY_WALKABLE_LAYER = 8;
		float maxRaycastDepth = 100f; 		// Hard coded value

		// ScreenRect
		Rect screenRectAtStartPlay = new Rect(0, 0, Screen.width, Screen.height);

		// New delegates
		// OnMouseOverTerrain(Vector3 destination)     va a pasar la info del mouse
		public delegate void OnMouseOverTerrain (Vector3 destination); 		// declare new delegate type
		public event OnMouseOverTerrain onMouseOverPotenciallyWalkable;
		
		// OnMouseOverEnemy(EnemyAI EnemyAI)			va a pasar la info del enemigo
		public delegate void OnMouseOverEnemy (EnemyAI EnemyAI); 		// declare new delegate type
		public event OnMouseOverEnemy onMouseOverEnemy;



		void Update()
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject()) // UI GameObject
            {
                // Implement UI interactio
            }
			else
			{
				PerformRaycast();
			}

        }
		
		void PerformRaycast()
		{
			if (screenRectAtStartPlay.Contains(Input.mousePosition))
			{	
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (RaycastForEnemy (ray)) {return; }

				if (RaycastForWalkable(ray)) {return; }
			}

		}

		private bool RaycastForEnemy(Ray ray)
        {
			// Do raycast, save into hitInfo variable.
			RaycastHit hitInfo;
			
			// Throw a bool Physic.Ray with: ray (from signature), out (to hitInfo), maxRay...
			Physics.Raycast (ray, out hitInfo, maxRaycastDepth);

			// Store the gameObject just hit in gameObjectHit variable.
			var gameObjectHit = hitInfo.collider.gameObject;
			
			// Store the EnemyAI.cs component in enemyHit variable, identify it by component
			var enemyHit = gameObjectHit.GetComponent<EnemyAI>();

			// If there is an enemyHit, if theres is a component EnemyAI Do this.
			if (enemyHit)
			{
				// Change cursor
				Cursor.SetCursor (enemyCursor, cursorHotspot, CursorMode.Auto);

				// Broadcast, onMouseOverEnemy with the EnemyAI reference.
				onMouseOverEnemy(enemyHit);

				// if we succed
				return true;
			}
			// If fail
			return false;

        }
        

		private bool RaycastForWalkable(Ray ray)
        {
			// Do raycast, save into hitInfo variable.
			RaycastHit hitInfo;
			
			// Make a LayerMask called potentiallyWalkable as far as POTENTIALLY_WALKABLE_LAYER
			LayerMask potentiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER;
			
			// Is it potentially walkable? if it is Save it in a bool called potenciallyWalkableHit
			bool potenciallyWalkableHit = Physics.Raycast (ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer);
			
			// If the bool potenciallyWalkableHit is true Do this.
			if (potenciallyWalkableHit)
			{
				// Set the cursor according to this Image
				Cursor.SetCursor (walkCursor, cursorHotspot, CursorMode.Auto);
				
				// Call the Delegate, pass the RaycastHit hitInfo but the point (a Vector3 taken from RaycastHit)
				onMouseOverPotenciallyWalkable(hitInfo.point);
				
				// if succeed return true;
				return true;
			}
			
			// return false if it doesn't
			return false;
			
        }


	}

}
