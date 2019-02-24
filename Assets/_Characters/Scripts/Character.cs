using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters
{
    [SelectionBase]
    public class Character : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Avatar characterAvatar;
        [SerializeField] [Range (.1f, 1f)] float animatorHalfWay = 1f;

        [Header("Movement")]
        //[SerializeField] float stoppingDistance = 1f;
        [SerializeField] float moveSpeedMultiplier = 0.7f; 
        [SerializeField] float animationSpeedMultiplier = 1.5f;
        [SerializeField] float movingTurnSpeed = 360;
		[SerializeField] float stationaryTurnSpeed = 180;
		[SerializeField] float moveThreshold = 1f;
        float turnAmount;
		float forwardAmount;

        [Header("Capsule Collider")]
        [SerializeField] Vector3 colliderCentre = new Vector3(0, 1.04f, 0);
        [SerializeField] float colliderRadius = 0.22f;
        [SerializeField] float colliderHeigth = 2.07f;
        
        [Header("Audio")]
        [SerializeField] float audioSourceSpatialBlend = 1;
        
        [Header("Navmesh Agent")]
        [SerializeField] float navMeshAgentSteeringSpeed = 1.62f;
        [SerializeField] float navMeshAgentStoppingDistance = 1.44f;
        [SerializeField] float navMeshAgentBaseOffset = -0.02f;
        [SerializeField] float navMeshAgentRadius = 0.26f;
        NavMeshAgent navMeshAgent;
        
        Animator animator;
        Rigidbody myRigidbody;

        bool isAlive = true;

        void Awake()
        {
            AddRequiredComponents();
        }

        private void AddRequiredComponents()
        {
            animator = gameObject.AddComponent<Animator>(); // ThirdPersonAnimatorController, skeleton_staticAvatar 
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvatar;

            myRigidbody = gameObject.AddComponent<Rigidbody>();
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>(); // centre 1.04, Radius 0,22, Height 2.07
            capsuleCollider.center = colliderCentre;
            capsuleCollider.radius = colliderRadius;
            capsuleCollider.height = colliderHeigth;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = audioSourceSpatialBlend;

            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();

            navMeshAgent.updatePosition = true;
            navMeshAgent.updateRotation = true;
            navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
            navMeshAgent.baseOffset = navMeshAgentBaseOffset;
            navMeshAgent.radius = navMeshAgentRadius;
            navMeshAgent.speed = navMeshAgentSteeringSpeed;
            navMeshAgent.autoBraking = false;
        }

        void Update()
        {
            if (!navMeshAgent.isOnNavMesh)
            {
                Debug.LogError (gameObject.name + "is not on the navmesh");
            }

            else if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && isAlive)
            {
                Move(navMeshAgent.desiredVelocity);
            }
            else
            {
                Move (Vector3.zero);
            }
        }

        public void SetDestination(Vector3 worldPos)
        {
            navMeshAgent.destination = worldPos;
        }

        public AnimatorOverrideController GetAnimatorOverrideController ()
        {
            return animatorOverrideController;
        }

        public float GetAnimSpeedMultiplier()
        {
            return animationSpeedMultiplier;
        }

        void Move(Vector3 movement)
        {
            SetForwardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        public void Kill()
        {
            isAlive = false;
            navMeshAgent.isStopped = true;
        }

        void OnAnimatorMove()
        {
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                velocity.y = myRigidbody.velocity.y;
                myRigidbody.velocity = velocity;
            }

        }

        void SetForwardAndTurn(Vector3 movement)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired direction.
            if (movement.magnitude > moveThreshold)
            {
                movement.Normalize();
            }

            var localMove = transform.InverseTransformDirection(movement);

            // break that movement in components
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
			
            forwardAmount = localMove.z;
        }

        void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
			transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
		}

        void UpdateAnimator()
		{
			animator.SetFloat("Forward", forwardAmount * animatorHalfWay, 0.1f, Time.deltaTime);
			animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = animationSpeedMultiplier;
        
        }

    }

}