////////////using UnityEngine;
////////////using UnityEngine.InputSystem;
////////////namespace TopDown.CameraControl
////////////{
////////////    public class CameraController : MonoBehaviour
////////////    {
////////////        [SerializeField] private Transform playerTransform;
////////////        [SerializeField] private float displacementMultiplier = 0.15f;
////////////        private float zPosition = -10;

////////////        private void Update()
////////////        {
////////////            if (Mouse.current == null) return;

////////////            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

////////////            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(transform.position.z)));

////////////            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;

////////////            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement;
////////////            finalCameraPosition.z = zPosition;
////////////            transform.position = finalCameraPosition;
////////////        }
////////////    }
////////////}

//////////using UnityEngine;

//////////namespace TopDown.CameraControl
//////////{
//////////    public class CameraController : MonoBehaviour
//////////    {
//////////        [SerializeField] private Transform target;
//////////        [SerializeField] private float smoothSpeed = 5f;
//////////        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

//////////        // LateUpdate is standard for cameras to prevent stuttering
//////////        private void LateUpdate()
//////////        {
//////////            // THE FIX: If the player is destroyed or disabled, do nothing.
//////////            if (target == null || !target.gameObject.activeInHierarchy)
//////////            {
//////////                return;
//////////            }

//////////            Vector3 desiredPosition = target.position + offset;
//////////            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//////////            transform.position = smoothedPosition;
//////////        }
//////////    }
//////////}

////////using UnityEngine;

////////namespace TopDown.CameraControl
////////{
////////    public class CameraController : MonoBehaviour
////////    {
////////        [SerializeField] private Transform target;
////////        [SerializeField] private float smoothSpeed = 5f;
////////        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

////////        private void LateUpdate()
////////        {
////////            // If the player is destroyed OR hidden, the camera just stays put.
////////            // This prevents it from flying away or throwing errors.
////////            if (target == null) return;

////////            Vector3 desiredPosition = target.position + offset;
////////            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
////////        }
////////    }
////////}

//////using UnityEngine;

//////namespace TopDown.CameraControl
//////{
//////    public class CameraController : MonoBehaviour
//////    {
//////        [SerializeField] private Transform target;
//////        [SerializeField] private float smoothSpeed = 5f;
//////        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

//////        private void LateUpdate()
//////        {
//////            // Safety check: if the player object is missing, stop the script here.
//////            if (target == null) return;

//////            Vector3 desiredPosition = target.position + offset;
//////            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//////        }
//////    }
//////}

////using UnityEngine;

////namespace TopDown.CameraControl
////{
////    public class CameraController : MonoBehaviour
////    {
////        [Header("Follow Settings")]
////        [SerializeField] private Transform target;
////        [SerializeField] private float smoothSpeed = 5f;
////        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

////        [Header("Mouse Influence")]
////        [SerializeField] private float mouseInfluence = 0.3f; // How much the cursor pulls the camera

////        private void LateUpdate()
////        {
////            // SAFETY: If player is dead/gone, stop moving to avoid the "Ruined" feel
////            if (target == null) return;

////            // 1. Calculate Player Position
////            Vector3 playerPos = target.position + offset;

////            // 2. Calculate Mouse Influence (The "Beautiful" part you had)
////            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
////            Vector3 targetPos = Vector3.Lerp(playerPos, mousePos, mouseInfluence);
////            targetPos.z = offset.z; // Keep the camera at the correct depth

////            // 3. Smooth Move
////            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.InputSystem; // Added to support New Input System

//namespace TopDown.CameraControl
//{
//    public class CameraController : MonoBehaviour
//    {
//        [Header("Follow Settings")]
//        [SerializeField] private Transform target;
//        [SerializeField] private float smoothSpeed = 5f;
//        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

//        [Header("Mouse Influence")]
//        [SerializeField][Range(0f, 1f)] private float mouseInfluence = 0.3f;

//        private void LateUpdate()
//        {
//            // SAFETY: If player is dead, stay on the spot where they died
//            if (target == null || !target.gameObject.activeInHierarchy) return;

//            // 1. Calculate Player Position
//            Vector3 playerPos = target.position + offset;

//            // 2. FIXED: New Input System way to get Mouse Position
//            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
//            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -offset.z));

//            // 3. Combine Player + Mouse for that "Cursor Influence" feel
//            Vector3 targetPos = Vector3.Lerp(playerPos, mouseWorldPos, mouseInfluence);
//            targetPos.z = offset.z; // Ensure camera doesn't fly into 2D plane

//            // 4. Smooth Follow
//            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
//        }
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem; // Required for the New Input System

namespace TopDown.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float displacementMultiplier = 0.15f;
        [SerializeField] private float smoothSpeed = 5f;
        private float zPosition = -10;

        private void LateUpdate()
        {
            // Safety: If the player is dead/inactive, stay put
            if (playerTransform == null || !playerTransform.gameObject.activeInHierarchy) return;

            // 1. Get Mouse Position using New Input System
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

            // 2. Convert to World Space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -zPosition));

            // 3. Apply your perfect Displacement Multiplier logic
            Vector3 targetPos = playerTransform.position + (mouseWorldPos - playerTransform.position) * displacementMultiplier;
            targetPos.z = zPosition;

            // 4. Smooth Move
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        }
    }
}