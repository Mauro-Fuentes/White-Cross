using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI; // TODO: decoupling o rewiring

namespace RPG.Characters
{
    [RequireComponent (typeof (NavMeshAgent))]
    [RequireComponent (typeof (AICharacterControl))]
    [RequireComponent (typeof (ThirdPersonCharacter))]   // Script con la clase ThirdPersonCharacter
    public class PlayerMovement : MonoBehaviour
    {
        
        ThirdPersonCharacter thirdPersonCharacter = null;      // A reference to the ThirdPersonCharacter on the object
        CameraRaycaster cameraRaycaster = null;                // A reference to the CameraRaycaster class 
        AICharacterControl aiCharacterControl = null;
        GameObject walkTarget = null;
        
        
        NavMeshAgent navmeshdebug = null; // TODO: erase

        Vector3 clickPoint;

        bool inInDirectMode = false;
        
        void Start()
        {   
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();    // fetch the component class ThirdPersonCharacter
            
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();  // fetch the component class CameraRaycaster

            aiCharacterControl = GetComponent<AICharacterControl>();        // fetch the class AICharacterControl
            
            walkTarget = new GameObject("walkTarget");
            
        
            cameraRaycaster.onMouseOverPotenciallyWalkable += OnMouseOverPotenciallyWalkable;    

            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void OnMouseOverPotenciallyWalkable (Vector3 destination)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown (1))
            {
                walkTarget.transform.position = destination;
                aiCharacterControl.SetTarget (walkTarget.transform);
            }

        }


        void OnMouseOverEnemy (Enemy enemy)
        {
            if (Input.GetMouseButton(0))
            {
                aiCharacterControl.SetTarget (enemy.transform);
            }
        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown (KeyCode.G))
            {
                inInDirectMode = !inInDirectMode;
            }
            if (inInDirectMode)
            {
                ProcessDirectMovement();
            }
            else
            {
                return;
            }
        }

        private void ProcessDirectMovement()
        {
            //aiCharacterControl.enabled = false;
            // navmeshdebug = GetComponent<NavMeshAgent>();
            // navmeshdebug.enabled = false;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 cameraForward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1,0,1)).normalized;
            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            thirdPersonCharacter.Move (movement, false, false);
        }


    }

}