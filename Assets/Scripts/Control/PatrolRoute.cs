using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class PatrolRoute : MonoBehaviour
    {
        Transform[] _waypoints;

        private void Start()
        {
            
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Color myColor = new Color(1f, 0.92f, 0.016f, .8f);
                Gizmos.color = myColor;
                Gizmos.DrawSphere(transform.GetChild(i).position, .5f);
   
            }

        }
    }

}

