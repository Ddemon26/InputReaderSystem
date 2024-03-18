using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;
using static PlayerInput;

namespace ScriptableArchitect.InputSettings
{
    /// <summary>
    /// A ScriptableObject that reads and processes player input using the new Unity Input System.
    /// It raises events based on the input actions defined in the PlayerInput asset.
    /// </summary>
    [CreateAssetMenu(fileName = "InputReader", menuName = "PlayerController/InputReader")]
    public class InputReader : ScriptableObject, ICharacterControlsActions
    {
        private PlayerInput inputActions;

        // Movement events
        public event UnityAction<Vector2> Move = delegate { };
        public Vector3 Direction => inputActions.CharacterControls.Move.ReadValue<Vector2>();

        // Rotation events
        public event UnityAction<Vector2, bool> Rotate = delegate { };
        public event UnityAction<Vector2, bool> RotateController = delegate { };

        // Scroll event
        public event UnityAction<float> ScrollWheel = delegate { };

        // Binary input events (true when pressed, false when released)
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Run = delegate { };
        public event UnityAction<bool> Attack = delegate { };
        public event UnityAction<bool> Crouch = delegate { };
        public event UnityAction<bool> Block = delegate { };
        public event UnityAction<bool> Interact = delegate { };
        public event UnityAction<bool> Escape = delegate { };
        public event UnityAction<bool> OpenUI = delegate { };
        public event UnityAction<bool> Emote = delegate { };
        public event UnityAction<bool> CommandKey = delegate { };

        #region InputActions Initialization

        // Initializes the input actions when the script is enabled.
        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInput();
                inputActions.CharacterControls.SetCallbacks(this);
            }
        }

        // Enables the input actions for the player.
        public void EnablePlayerActions()
        {
            inputActions.Enable();
        }

        // Disables the input actions for the player.
        public void DisablePlayerActions()
        {
            inputActions.Disable();
        }

        #endregion

        #region Helper Methods

        // Checks if the input device is a mouse.
        private bool IsMouseDevice(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

        // Checks if the input device is a gamepad.
        private bool IsGamepadDevice(InputAction.CallbackContext context) => context.control.device.name.Contains("Gamepad");

        // Handles binary input actions (pressed/released).
        private void HandleBinaryAction(InputAction.CallbackContext context, UnityAction<bool> action)
        {
            if (context.phase == InputActionPhase.Started)
                action.Invoke(true);
            else if (context.phase == InputActionPhase.Canceled)
                action.Invoke(false);
        }

        #endregion

        #region ReadValue Actions

        // Invoked when the Move action is performed.
        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        // Invoked when the ScrollWheel action is performed.
        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            ScrollWheel.Invoke(context.ReadValue<float>());
        }

        // Invoked when the RotatePlayer action is performed with a mouse.
        public void OnRotatePlayer(InputAction.CallbackContext context)
        {
            Rotate.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        }

        // Invoked when the RotatePlayerController action is performed with a gamepad.
        public void OnRotatePlayerController(InputAction.CallbackContext context)
        {
            RotateController.Invoke(context.ReadValue<Vector2>(), IsGamepadDevice(context));
        }

        #endregion 

        #region Button Actions

        // Invoked when the Run action is performed.
        public void OnRun(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Run);
        }

        // Invoked when the Jump action is performed.
        public void OnJump(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Jump);
        }

        // Invoked when the Attack action is performed.
        public void OnAttack(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Attack);
        }

        // Invoked when the Block action is performed.
        public void OnBlock(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Block);
        }

        // Invoked when the Crouch action is performed.
        public void OnCrouch(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Crouch);
        }

        // Invoked when the Interact action is performed.
        public void OnInteract(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Interact);
        }

        // Invoked when the Escape action is performed.
        public void OnEscape(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Escape);
        }

        // Invoked when the OpenUI action is performed.
        public void OnOpenUI(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, OpenUI);
        }

        // Invoked when the Emote action is performed.
        public void OnEmote(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Emote);
        }

        // Invoked when the CommandKey action is performed.
        public void OnCommandKey(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, CommandKey);
        }

        #endregion
    }
}
