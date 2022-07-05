using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class PatrolRoute : MonoBehaviour
    {
        int _waypoints;
        [SerializeField] float waypointGizmoRadius = .3f;

        private void OnDrawGizmos()
        {

            _waypoints = transform.childCount;

            for (int i = 0; i < _waypoints; i++)
            {
                Color myColor = new Color(1f, 0.92f, 0.016f, .8f);
                Gizmos.color = myColor;

                // Drawing waypoints gizmos sphere
                Gizmos.DrawSphere(GetWapoint(i), waypointGizmoRadius);

                //Drawing line between waypoints (circular array)            
                Gizmos.DrawLine(GetWapoint(i), GetWapoint(GetNextIndex(i)));             

            }

        }

        public Vector3 GetWapoint(int i)
        {
            return transform.GetChild(i).position;
        }

        
        public int GetNextIndex(int i)
        {   
            // Like a circular array
            if ((i + 1) % _waypoints == 0)
                return 0;
            else
                return i + 1;
        }

    }

}

