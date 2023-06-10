using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIcontroller : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter Fighter;
        GameObject player;
        Health health;
        private void Start()
        {
            player = findPlayer();
            Fighter = GetComponent<Fighter>();  
            health = GetComponent<Health>();    
        }
        private void Update()
        {
            if (health.IsDead()) {return; }
 
            if (DistanceToPlayer() <= chaseDistance)
            {
                    attackPlayer();
            }
            else
            {
                    Fighter.Cancel();
            }
        }

        private void attackPlayer()
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

        private static GameObject findPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }
}
