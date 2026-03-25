using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : Movement
    {
        Animator anim;

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        // Get input
        private void OnMove(InputValue value)
        {
            Vector3 playerInput = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0);
            currentInput = playerInput;
        }

        void Update()
        {
            float speed = currentInput.magnitude;
            anim.SetBool("bool_run", speed > 0.1f);
        }
    }
}