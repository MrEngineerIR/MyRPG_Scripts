using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] float WeaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        MovementHandler movementHandler;
        Health target;
        float timeSiceLastAttack = Mathf.Infinity;
        private void Start()
        {
              movementHandler  = GetComponent<MovementHandler>();   
        }
        private void Update()
        {
            timeSiceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) { return; }
           if (!GetIsInRange())
           {
                movementHandler.MoveTo(target.transform.position , 1f);
           }
           else
           {
                  movementHandler.Cancel();
                  AttackBihaviour();
           }
            
           


        }

        private void AttackBihaviour()
        {
            transform.LookAt(target.transform);
            if (timeSiceLastAttack >= timeBetweenAttacks)
            {
                //this will trigger the hit event
                TriggerAttack();
                timeSiceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(WeaponDamage);
        }
        public bool CanAttack(GameObject target)
        {
            if (target == null) { return false; }
           Health targetToTest = target.GetComponent<Health>();
           return targetToTest!= null && !targetToTest.IsDead();
        }
        private bool GetIsInRange()
        {
            return (Vector3.Distance(transform.position, target.transform.position) < WeaponRange);
        }

        public void attack(GameObject CombatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = CombatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<MovementHandler>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
