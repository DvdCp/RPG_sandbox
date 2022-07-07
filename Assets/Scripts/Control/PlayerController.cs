﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover _mover;
        Fighter _fighter;
        Health _health;


        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();   
        }

        private void Update()
        {

            if (_health.IsDead) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
           
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                // if I can't attack target, go on ...
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                // ... else attack target until mouse in pressed down
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    // Going full speed to hit.point
                    GetComponent<Mover>().StartMovementAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //Ray debugRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(debugRay.origin, debugRay.direction * 100);
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
