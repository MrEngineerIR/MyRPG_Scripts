using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform Targer;
   
    void LateUpdate()
    {
        transform.position = Targer.position;
    }
}
