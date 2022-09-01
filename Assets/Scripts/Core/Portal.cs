using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core 
{ 
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                SceneManager.LoadScene(1);
        }
    }

}

