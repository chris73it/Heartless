using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace HeroicArcade.CC.Core
{
    /*
    public class MoveInputEvent : UnityEvent<Vector2>
    {
    }
    */
    [System.Serializable] public class SlideInputEvent : UnityEvent<bool>
    {
    }

    [System.Serializable] public class JumpInputEvent : UnityEvent<bool>
    {
    }
    [System.Serializable] public class ForwardInputEvent : UnityEvent<bool>
    {
    }
    [System.Serializable]  public class BackInputEvent : UnityEvent<bool>
    {
    }
    public sealed class InputController : MonoBehaviour
    {

        [SerializeField] SlideInputEvent slideInputEvent;
        [SerializeField] JumpInputEvent jumpInputEvent;
        [SerializeField] ForwardInputEvent forwardInputEvent;
        [SerializeField] BackInputEvent backInputEvent;


        public Controls controls;
        private void Awake()
        {
            controls = new Controls();

            controls.Gameplay.Slide.started += OnSlide;
            //controls.Gameplay.Slide.performed += OnSlide;
            controls.Gameplay.Slide.canceled += OnSlide;

            controls.Gameplay.Jump.started += OnJump;
            controls.Gameplay.Jump.canceled += OnJump;

            controls.Gameplay.Forward.started += OnForward;
            controls.Gameplay.Forward.canceled += OnForward;

            controls.Gameplay.Back.started += OnBack;
            controls.Gameplay.Back.canceled += OnBack;
        }

        //private Vector2 moveInput;
        [HideInInspector] bool IsSlidePressed;
        private void OnSlide(InputAction.CallbackContext context)
        {

            IsSlidePressed = context.ReadValueAsButton();
            slideInputEvent.Invoke(IsSlidePressed);
            /*
            moveInput = context.ReadValue<Vector2>();
            moveInputEvent.Invoke(moveInput);
            */
        }

        [HideInInspector] bool IsJumpPressed;
        private void OnJump(InputAction.CallbackContext context)
        {
            IsJumpPressed = context.ReadValueAsButton();
            jumpInputEvent.Invoke(IsJumpPressed);
        }

        [HideInInspector] bool IsForwardPressed;
        private void OnForward(InputAction.CallbackContext context)
        {
            IsForwardPressed = context.ReadValueAsButton();
            forwardInputEvent.Invoke(IsForwardPressed);
        }

        [HideInInspector] bool IsBackPressed;
        private void OnBack(InputAction.CallbackContext context)
        {
            IsBackPressed = context.ReadValueAsButton();
            backInputEvent.Invoke(IsBackPressed);
        }


        private void OnEnable()
        {
            controls.Gameplay.Enable();
            controls.InGameMenu.Disable();
        }

        private void OnDisable()
        {
            controls.Gameplay.Disable();
            controls.InGameMenu.Disable();
        }
    }
}