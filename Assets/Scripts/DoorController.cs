////////using UnityEngine;

////////namespace TopDown.World
////////{
////////    public class DoorController : MonoBehaviour
////////    {
////////        [Header("Door Settings")]
////////        [SerializeField] private bool isOpen = false;
////////        [SerializeField] private float relativeOpenAngle = -90f; // How much it swings relative to start
////////        [SerializeField] private float smoothSpeed = 5f;

////////        private Quaternion closedRotation;
////////        private Quaternion openRotation;
////////        private Quaternion targetRotation;

////////        private void Awake()
////////        {
////////            // 1. Capture the rotation you set in the Editor as "Closed"
////////            closedRotation = transform.localRotation;

////////            // 2. Calculate "Open" by adding the relative angle to the closed rotation
////////            openRotation = closedRotation * Quaternion.Euler(0, 0, relativeOpenAngle);

////////            // 3. Set the starting target
////////            targetRotation = isOpen ? openRotation : closedRotation;

////////            // Snap to the starting position immediately
////////            transform.localRotation = targetRotation;
////////        }

////////        private void Update()
////////        {
////////            // Smoothly move toward whichever state we are in
////////            transform.localRotation = Quaternion.Slerp(
////////                transform.localRotation,
////////                targetRotation,
////////                Time.deltaTime * smoothSpeed
////////            );
////////        }

////////        public void ToggleDoor()
////////        {
////////            isOpen = !isOpen;
////////            targetRotation = isOpen ? openRotation : closedRotation;

////////            Debug.Log($"{gameObject.name} Toggled. Open: {isOpen}");
////////        }
////////    }
////////}

//////using UnityEngine;

//////namespace TopDown.World
//////{
//////    public class DoorController : MonoBehaviour
//////    {
//////        [Header("Door Settings")]
//////        [SerializeField] private bool isOpen = false;
//////        [SerializeField] private float relativeOpenAngle = -90f;
//////        [SerializeField] private float smoothSpeed = 5f;

//////        private Quaternion closedRotation;
//////        private Quaternion openRotation;
//////        private Quaternion targetRotation;

//////        private void Awake()
//////        {
//////            // Capture the rotation you set in the Editor as "Closed"
//////            closedRotation = transform.localRotation;
//////            openRotation = closedRotation * Quaternion.Euler(0, 0, relativeOpenAngle);

//////            // Initial state
//////            targetRotation = isOpen ? openRotation : closedRotation;
//////            transform.localRotation = targetRotation;
//////        }

//////        private void OnValidate()
//////        {
//////            // This runs whenever you change a value in the Inspector
//////            if (Application.isPlaying)
//////            {
//////                targetRotation = isOpen ? openRotation : closedRotation;
//////            }
//////        }

//////        private void Update()
//////        {
//////            // FORCE the target to match the checkbox (in case ToggleDoor isn't called)
//////            targetRotation = isOpen ? openRotation : closedRotation;

//////            // Smoothly move toward whichever state we are in
//////            transform.localRotation = Quaternion.Slerp(
//////                transform.localRotation,
//////                targetRotation,
//////                Time.deltaTime * smoothSpeed
//////            );
//////        }

//////        public void ToggleDoor()
//////        {
//////            isOpen = !isOpen;
//////            targetRotation = isOpen ? openRotation : closedRotation;
//////            Debug.Log($"{gameObject.name} Toggled. Open: {isOpen}");
//////        }
//////    }
//////}

////using UnityEngine;

////namespace TopDown.World
////{
////    public class DoorController : MonoBehaviour
////    {
////        [Header("Door Settings")]
////        [SerializeField] private bool isOpen = false;
////        [SerializeField] private float relativeOpenAngle = -90f;
////        [SerializeField] private float smoothSpeed = 5f;

////        private float closedZ;
////        private float openZ;
////        private float targetZ;

////        private void Awake()
////        {
////            // 1. Capture ONLY the Z rotation you set in the Scene
////            closedZ = transform.localRotation.eulerAngles.z;

////            // 2. Calculate the Open Z
////            openZ = closedZ + relativeOpenAngle;

////            // 3. Set starting target
////            targetZ = isOpen ? openZ : closedZ;

////            // Snap to initial position
////            transform.localRotation = Quaternion.Euler(0, 0, targetZ);
////        }

////        private void Update()
////        {
////            // Always sync targetZ with the checkbox (for debugging)
////            targetZ = isOpen ? openZ : closedZ;

////            // Smoothly rotate ONLY on the Z axis
////            float currentZ = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.z, targetZ, smoothSpeed * 50f * Time.deltaTime);
////            transform.localRotation = Quaternion.Euler(0, 0, currentZ);
////        }

////        public void ToggleDoor()
////        {
////            isOpen = !isOpen;
////            // No need to set targetZ here, Update() handles it via the isOpen bool
////            Debug.Log($"<color=green>{gameObject.name} Toggled. Open: {isOpen}</color>");
////        }
////    }
////}

//using UnityEngine;

//namespace TopDown.World
//{
//    public class DoorController : MonoBehaviour
//    {
//        public bool isOpen = false; // LevelController will flip this switch
//        [SerializeField] private float openAngle = -90f;
//        [SerializeField] private float smoothSpeed = 5f;

//        private Quaternion closedRotation;
//        private Quaternion targetRotation;

//        private void Awake()
//        {
//            closedRotation = transform.localRotation;
//        }

//        private void Update()
//        {
//            // Calculate where we WANT to be based on the checkbox
//            Quaternion desiredRotation = closedRotation * Quaternion.Euler(0, 0, isOpen ? openAngle : 0f);

//            // Move there smoothly
//            transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, Time.deltaTime * smoothSpeed);
//        }
//    }
//}

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