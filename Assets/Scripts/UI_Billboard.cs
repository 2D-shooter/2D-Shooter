using UnityEngine;

namespace TopDown.UI
{
    public class UI_Billboard : MonoBehaviour
    {
        [SerializeField] private bool useStaticRotation = true;
        private Quaternion fixedRotation;

        private void Awake()
        {
            // Capture the rotation it has at the start (usually 0,0,0)
            fixedRotation = transform.rotation;
        }

        // LateUpdate runs after the Enemy moves/rotates
        private void LateUpdate()
        {
            if (useStaticRotation)
            {
                // Force the bar to stay at the starting rotation
                transform.rotation = fixedRotation;
            }
            else
            {
                // Alternative: Always face the camera
                transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                                 Camera.main.transform.rotation * Vector3.up);
            }
        }
    }
}