using System;
using UnityEngine;

namespace RPG.Characters    //where unity goes and look up the script
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]    //needed component
    [RequireComponent(typeof (ThirdPersonCharacter))]   
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }  // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; }     // the character we are controlling //THE SCRIPT ACTUALLY
        public Transform target;                                        // target to aim for


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            { agent.SetDestination(target.position); }

            if (agent.remainingDistance > agent.stoppingDistance)
            {   character.Move(agent.desiredVelocity, false, false); }

            else
            { 
                //  if (GetComponent<Enemy>())
                if (GetComponent<Enemy>())
                {   
                    agent.velocity = Vector3.zero;
                }
                character.Move(Vector3.zero, false, false);

            }
                
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
