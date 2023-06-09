using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPG.Combat
{
    
    public class Health : MonoBehaviour
    {
        bool isDead;
        private void Start()
        {
            isDead = false;
        }
        [SerializeField] float healthPoints;

        public void TakeDamage(float damage)
        {
           healthPoints  = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0 && !isDead)
            {
                Die();
            }
        }
        public bool IsDead()
        {
            return isDead;  
        }

        private void Die()
        {
            if (isDead) { return; }
            isDead = true;
           // GetComponent<NavMeshAgent>().enabled = false;
            //GetComponent<CapsuleCollider>().enabled = false;    
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}

