using UnityEngine;
using UnityEngine.InputSystem; // NEW: Added this namespace
using TopDown.World;

namespace TopDown.Core
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float interactRange = 2f;
        [SerializeField] private LayerMask interactableLayer;

        // This method is called by the "Player Input" component if you use Messages
        // OR you can check the keyboard directly like this for a quick prototype:
        void Update()
        {
            // The New Input System's way of checking a single key press
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                CheckForInteractables();
            }
        }

        private void CheckForInteractables()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);

            foreach (var hit in hitColliders)
            {
                if (hit.TryGetComponent<DoorController>(out var door))
                {
                    door.ToggleDoor();
                    break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}