using UnityEngine;

namespace DivinityPFA.Systems
{
    [RequireComponent(typeof(BoxCollider))]
    public class CheckPointBehaviour : MonoBehaviour, ISaveable
    {
        private bool isPicked = false;

        // ISaveable Interface
        public object CaptureState ()
        {
            return isPicked;
        }
        public void RestoreState (object state)
        {
            isPicked = (bool)state;
        }

        private void OnTriggerEnter (Collider other)
        {
            if (isPicked)
            {
                this.enabled = false;
                return;
            }

            if (other.CompareTag("Player"))
            {
                GameManager.instance.CheckPointPicked(this.transform);
                isPicked = true;
            }
        }
    }
}