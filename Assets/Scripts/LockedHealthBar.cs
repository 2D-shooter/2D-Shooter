using UnityEngine;

namespace TopDown.UI
{
    public class LockedHealthBar : MonoBehaviour
    {
        [Header("Settings")]
        // This line creates the "Target" slot in your Inspector
        [SerializeField] private Transform target;

        [SerializeField] private Vector3 offset = new Vector3(0, 2.5f, 0);

        private Quaternion fixedRotation;

        private void Start()
        {
            // Lock the rotation to whatever it is at the start (usually 0,0,0)
            fixedRotation = transform.rotation;

            // If you forget to drag the Villain into the slot, this tries to find it automatically
            if (target == null && transform.parent != null)
            {
                target = transform.parent;
            }
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // 1. Force the position to stay centered on the Villain + Offset
            // This stops the "Orbiting"
            transform.position = target.position + offset;

            // 2. Force the rotation to stay flat
            // This stops the "Spinning"
            transform.rotation = fixedRotation;
        }
    }
}