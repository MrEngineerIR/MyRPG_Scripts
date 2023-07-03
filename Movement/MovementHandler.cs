
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class MovementHandler : MonoBehaviour , IAction
    {

        NavMeshAgent meshAgent;
        Animator animator;
        Health health;
        [SerializeField] float maxSpeed = 6f;
        Vector3 Speed;
        void Start()
        {
            meshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();    

        }

        void Update()
        {
            meshAgent.enabled = !health.IsDead();
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = meshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }


        public void StartMoveAction(Vector3 destination , float speedFraction)
        {
            GetComponent<ActionScheduler>().startAction(this);
            MoveTo(destination, speedFraction);
            
        }
        public void MoveTo(Vector3 destination , float speedFraction)
        {
            meshAgent.isStopped = false;
            meshAgent.destination = destination;
            meshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            
        }
        public void Cancel()
        {
            meshAgent.isStopped = true; 
        }
    }
}

