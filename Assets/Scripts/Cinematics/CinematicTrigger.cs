using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayed && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
            
        } 
    }

}


