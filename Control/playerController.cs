using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class playerController : MonoBehaviour
    {

        MovementHandler MovementHandler;

        private void Start()
        {
            MovementHandler = GetComponent<MovementHandler>();
        }
        private void Update()
        {
            if (InterfaceWhitCombat()) return;
            if (InterfaceWhitMove()) return;
           
        }

        private bool InterfaceWhitCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().attack(target);
                    target = null;
                }
                return true;
            }
            return false;
        }

        private bool InterfaceWhitMove()
        {
            RaycastHit hitInfo;
            Physics.Raycast(GetMouseRay(), out hitInfo, 500);
            if (hitInfo.collider)
            {
                if (Input.GetMouseButton(0))
                {
                    MovementHandler.StartMoveAction(hitInfo.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

