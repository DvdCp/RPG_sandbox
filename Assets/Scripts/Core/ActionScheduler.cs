using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                print($"Cancelling {action}");
                currentAction.Cancel();
            }

            currentAction = action;
        }

        public void CancelCurrentAction()
        { 
            // Just annuling current action... No update of currentAction needed
            StartAction(null);

        }
    }
}