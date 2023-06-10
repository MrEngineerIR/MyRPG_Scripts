using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float WaypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            
            for (int i = 0; i < transform.childCount; i++)
            {

                int J = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), WaypointGizmoRadius);
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(J).position);
            }
        }

        public  int GetNextIndex(int i)
        {
            if (i +1 == transform.childCount)
            {
                return 0; 
            }
             return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}
