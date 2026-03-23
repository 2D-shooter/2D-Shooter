

using UnityEngine;

namespace TopDown.World
{
    public class DoorController : MonoBehaviour
    {
        [Header("Door Settings")]
        public bool isOpen = false;
        [SerializeField] private float relativeOpenAngle = -90f;
        [SerializeField] private float smoothSpeed = 5f;

        private Quaternion closedRotation;

        private void Awake()
        {
            // Capture the rotation you set in the Scene as "Closed"
            closedRotation = transform.localRotation;
        }

        private void Update()
        {
            // Calculate where we WANT to be based on the isOpen bool
            Quaternion desiredRotation = closedRotation * Quaternion.Euler(0, 0, isOpen ? relativeOpenAngle : 0f);

            // Move there smoothly
            transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, Time.deltaTime * smoothSpeed);
        }

        // This is the function PlayerInteract was looking for!
        public void ToggleDoor()
        {
            isOpen = !isOpen;
            Debug.Log($"<color=orange>{gameObject.name} Toggled. IsOpen: {isOpen}</color>");
        }
    }
}