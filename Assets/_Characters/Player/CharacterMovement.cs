using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters
{
    [RequireComponent (typeof (NavMeshAgent))]
    [RequireComponent (typeof (ThirdPersonCharacter))]   // Script con la clase ThirdPersonCharacter
    public class CharacterMovement : MonoBehaviour
    {

        [SerializeField] float stoppingDistance = 1f;
        
        ThirdPersonCharacter character;                 // A reference to the ThirdPersonCharacter on the object
        Vector3 clickPoint;
        GameObject walkTarget;
        NavMeshAgent agent;
        
        void Start()
        {   
            CameraRaycaster cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>(); // fetch the component class CameraRaycaster
            character = GetComponent<ThirdPersonCharacter>(); // fetch the component class ThirdPersonCharacter

            walkTarget = new GameObject("walkTarget");

            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = true;
            agent.stoppingDistance = stoppingDistance;

            cameraRaycaster.onMouseOverPotenciallyWalkable += OnMouseOverPotenciallyWalkable;    
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void Update()
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity);
            }
            else
            {
                character.Move (Vector3.zero);
            }
        }

        void OnMouseOverPotenciallyWalkable (Vector3 destination)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown (1))
            {
                agent.SetDestination(destination);
            }

        }

        void OnMouseOverEnemy (Enemy enemy)
        {
            if (Input.GetMouseButton(0))
            {
                agent.SetDestination (enemy.transform.position);
            }
        }

        void FixedUpdate()
        {

        }

    }

}