using UnityEngine;
using TMPro; // Needed if you use TextMeshPro

namespace TopDown.World
{
    public class DoorController : MonoBehaviour
    {
        [Header("Door Settings")]
        public bool isOpen = false;
        [SerializeField] private float relativeOpenAngle = -90f;
        [SerializeField] private float smoothSpeed = 5f;

        [Header("UI Prompt")]
        [SerializeField] private GameObject interactionText; // Drag "Paina E" object here

        private Quaternion closedRotation;

        private void Awake()
        {
            // Capture the rotation you set in the Scene as "Closed"
            closedRotation = transform.localRotation;

            // Hide the text at the very start
            if (interactionText != null) interactionText.SetActive(false);
        }

        private void Update()
        {
            // Calculate where we WANT to be based on the isOpen bool
            Quaternion desiredRotation = closedRotation * Quaternion.Euler(0, 0, isOpen ? relativeOpenAngle : 0f);

            // Move there smoothly
            transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, Time.deltaTime * smoothSpeed);
        }

        // Trigger detection for the "Paina E" prompt
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && interactionText != null)
            {
                interactionText.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }

        // This is the function PlayerInteract is looking for!
        public void ToggleDoor()
        {
            isOpen = !isOpen;
            Debug.Log($"<color=orange>{gameObject.name} Toggled. IsOpen: {isOpen}</color>");
        }
    }
}