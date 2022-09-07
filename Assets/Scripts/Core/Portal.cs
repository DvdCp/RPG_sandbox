using System.Collections;
using UnityEngine;
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
                StartCoroutine(Transition(other.transform));
                           
        }

        private IEnumerator Transition(Transform player)
        {
            // Preventing destroy portal when the new scene is loading
            //DontDestroyOnLoad(gameObject);
            // P.N: this method doesn't work as we want in Unity 2021.3.3f1
            
            // Loading new scene in background
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Finding portal into new loaded scene
            Portal arrivingPortal = getNextPortal();

            // Updating player location
            player.position = arrivingPortal.spawnPoint.transform.position;
            
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

