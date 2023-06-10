using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using System;
using RPG.Movement;

namespace RPG.Control
{
    public class AIcontroller : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float TimeForSuspicionState = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float WaypointTolerance = 1f;
        [SerializeField] float waypointDewllTime = 3f;
        Fighter Fighter;
        MovementHandler MovementHandler;
        GameObject player;
        Health health;
        ActionScheduler actionScheduler;
        Vector3 GuardPosition;
        float TimeSinceLastSawEnemey = Mathf.Infinity;
        float TimeSinceArrivedAtWaypoint = Mathf.Infinity;
        int CurrentWaypointIndex = 0;
        private void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();  
            MovementHandler = GetComponent<MovementHandler>();  
            player = findPlayer();
            Fighter = GetComponent<Fighter>();  
            health = GetComponent<Health>();
            GuardPosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) { return; }

            if (DistanceToPlayer() <= chaseDistance)
            {
                TimeSinceLastSawEnemey = 0;
                AttackBehaviour();
            }
            else if (TimeForSuspicionState >= TimeSinceLastSawEnemey)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            TimeSinceArrivedAtWaypoint += Time.deltaTime;
            TimeSinceLastSawEnemey += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosision = GuardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    TimeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosision = GetCurrentWaypoint();
            }
            if (TimeSinceArrivedAtWaypoint > waypointDewllTime)
            {
                MovementHandler.StartMoveAction(nextPosision);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(CurrentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            CurrentWaypointIndex = patrolPath.GetNextIndex(CurrentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position , GetCurrentWaypoint());
            return distanceToWaypoint < WaypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.cancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            GameObject player =  findPlayer();
            if (Fighter.CanAttack(player))
            {
                Fighter.attack(player); 
            }
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }

        private  GameObject findPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
        //called by unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
    
}
