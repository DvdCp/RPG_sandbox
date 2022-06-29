using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;

        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                // Obtaining Ray from mouse and info about hit
                RaycastHit hit;
                bool hasHit = Physics.Raycast(GetRayFromMouse(), out hit);

                if (hasHit)
                { 
                    _mover.StartMovement(hit.point);
                    Combat(hit);
                }               
            }
        }

        private void Combat(RaycastHit mouseInput)
        {
            CombatAttacker target = mouseInput.collider.GetComponent<CombatAttacker>();
            if (target != null)
            {
                _fighter.Attack(target);
            }
        }

        private static Ray GetRayFromMouse()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
