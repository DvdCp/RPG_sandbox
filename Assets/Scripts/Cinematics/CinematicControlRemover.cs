using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _director;
        private GameObject _player;
        private PlayerController _playerController;

        // Start is called before the first frame update
        void Start()
        {
            _director = GetComponent<PlayableDirector>();
            _director.played += DisableControl;
            _director.stopped += EnableControl;

            _player = GameObject.FindWithTag("Player");
            _playerController = _player.GetComponent<PlayerController>();
        }

        void EnableControl(PlayableDirector director)
        {
            _playerController.enabled = true;
        }

        void DisableControl(PlayableDirector director)
        {
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _playerController.enabled = false;
        }
    }

}


