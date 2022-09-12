using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.Core 
{ 
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = 1;
        [SerializeField] Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                StartCoroutine(Transition());
                           
        }

        private IEnumerator Transition()
        {
            // Preventing destroy portal when the new scene is loading
            DontDestroyOnLoad(gameObject);
            
            // Loading new scene in background
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Finding portal into new loaded scene
            Portal arrivingPortal = getNextPortal();

            // Updating player location & rotation
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(arrivingPortal.spawnPoint.transform.position);
            //player.transform.position = arrivingPortal.spawnPoint.transform.position;
            player.transform.rotation = arrivingPortal.spawnPoint.transform.rotation;

            // Now destroy this portal
            Destroy(gameObject);

        }

        private Portal getNextPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            { 
                if (portal == this) continue;

                return portal;            
            }

            // if no portals are found
            return null;
        }
    }

}

