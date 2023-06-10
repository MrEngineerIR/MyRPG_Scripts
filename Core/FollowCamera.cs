using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform Targer;
        Quaternion prevoisQuaternion;
   
        private void Start()
        {
        
            prevoisQuaternion = transform.rotation; 
            transform.position = Targer.position;

        }
        void LateUpdate()
        {
            transform.position = Targer.position;
            if (Input.GetKey(KeyCode.Space))
            {
                transform.rotation = new Quaternion(Targer.rotation.x, 50, Targer.rotation.z, 0);
            }
            else if (Input.GetKey(KeyCode.N))
            {
                transform.rotation = prevoisQuaternion;
                
            }
           
          
        }
    }
}


