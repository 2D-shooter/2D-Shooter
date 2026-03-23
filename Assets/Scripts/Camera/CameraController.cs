

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