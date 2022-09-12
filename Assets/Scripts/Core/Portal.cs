using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.Core 
{ 
    public class Portal : MonoBehaviour
    {
        enum DestinationIdintifier
        { 
            A, B, C, D
        }

        [SerializeField] int sceneToLoad = 1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdintifier destination;

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

            UpdatePlayer(arrivingPortal);

            // Now destroy this portal
            Destroy(gameObject);

        }

        private static void UpdatePlayer(Portal arrivingPortal)
        {
            // Updating player location & rotation
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(arrivingPortal.spawnPoint.transform.position);
            player.transform.rotation = arrivingPortal.spawnPoint.transform.rotation;
        }

        private Portal getNextPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            { 
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;            
            }

            // if no portals are found
            return null;
        }
    }

}

