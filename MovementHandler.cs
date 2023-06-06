using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovementHandler : MonoBehaviour
{
   
    NavMeshAgent meshAgent;
    Animator animator;
    Vector3 Speed;
    void Start()
    {
        meshAgent  = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
       
    }

    void Update()
    {
      
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
           
        }
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector3 velocity = meshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

    private void MoveToCursor()
    {
        Ray lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(lastRay, out hitInfo, 500);
        if (hitInfo.collider)
        {
            meshAgent.destination = hitInfo.point;
        }
    }
}

