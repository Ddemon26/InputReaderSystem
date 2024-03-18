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
    [CreateAssetMenu(fileName = "InputReader", menuName = "PlayerController/InputReader"), Tooltip("A ScriptableObject that reads and processes player input using the new Unity Input System.")]
    public class InputReader : ScriptableObject, ICharacterControlsActions
    {
        private PlayerInput inputActions;

        // Movement events
        /// <summary>
        /// an event that is triggered when the player moves.
        /// </summary>
        public event UnityAction<Vector2> Move = delegate { };
        /// <summary>
        /// The direction the player is moving in.
        /// </summary>
        public Vector3 Direction => inputActions.CharacterControls.Move.ReadValue<Vector2>();

        // Rotation events
        /// <summary>
        /// an event that is triggered when the player rotates.
        /// </summary>
        public event UnityAction<Vector2, bool> Rotate = delegate { };
        /// <summary>
        /// an event that is triggered when the player rotates the controller.
        /// </summary>
        public event UnityAction<Vector2, bool> RotateController = delegate { };

        // Scroll event
        /// <summary>
        /// an event that is triggered when the player scrolls.
        /// </summary>
        public event UnityAction<float> ScrollWheel = delegate { };

        // Binary input events (true when pressed, false when released)
        /// <summary>
        /// an event that is triggered when the player jumps.
        /// </summary>
        public event UnityAction<bool> Jump = delegate { };
        /// <summary>
        /// an event that is triggered when the player runs.
        /// </summary>
        public event UnityAction<bool> Run = delegate { };
        /// <summary>
        /// an event that is triggered when the player attacks.
        /// </summary>
        public event UnityAction<bool> Attack = delegate { };
        /// <summary>
        /// an event that is triggered when the player crouches.
        /// </summary>
        public event UnityAction<bool> Crouch = delegate { };
        /// <summary>
        /// an event that is triggered when the player blocks.
        /// </summary>
        public event UnityAction<bool> Block = delegate { };
        /// <summary>
        /// an event that is triggered when the player interacts.
        /// </summary>
        public event UnityAction<bool> Interact = delegate { };
        /// <summary>
        /// an event that is triggered when the player escapes.
        /// </summary>
        public event UnityAction<bool> Escape = delegate { };
        /// <summary>
        /// an event that is triggered when the player opens the UI.
        /// </summary>
        public event UnityAction<bool> OpenUI = delegate { };
        /// <summary>
        /// an event that is triggered when the player emotes.
        /// </summary>
        public event UnityAction<bool> Emote = delegate { };
        /// <summary>
        /// an event that is triggered when the player uses the command key.
        /// </summary>
        public event UnityAction<bool> CommandKey = delegate { };

        #region InputActions Initialization

        /// <summary>
        /// Initializes the input actions when the script is enabled.
        /// </summary>
        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInput();
                inputActions.CharacterControls.SetCallbacks(this);
            }
        }

        /// <summary>
        /// Enables the input actions for the player.
        /// </summary>
        public void EnablePlayerActions()
        {
            inputActions.Enable();
        }

        /// <summary>
        /// Disables the input actions for the player.
        /// </summary>
        public void DisablePlayerActions()
        {
            inputActions.Disable();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Checks if the input device is a mouse.
        /// </summary>
        private bool IsMouseDevice(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

        /// <summary>
        /// Checks if the input device is a gamepad.
        /// </summary>
        private bool IsGamepadDevice(InputAction.CallbackContext context) => context.control.device.name.Contains("Gamepad");

        /// <summary>
        /// Handles binary input actions (pressed/released).
        /// </summary>
        private void HandleBinaryAction(InputAction.CallbackContext context, UnityAction<bool> action)
        {
            if (context.phase == InputActionPhase.Started)
                action.Invoke(true);
            else if (context.phase == InputActionPhase.Canceled)
                action.Invoke(false);
        }

        #endregion

        #region ReadValue Actions

        /// <summary>
        /// Invoked when the Move action is performed.
        /// </summary>
        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Invoked when the ScrollWheel action is performed.
        /// </summary>
        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            ScrollWheel.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Invoked when the RotatePlayer action is performed with a mouse.
        /// </summary>
        public void OnRotatePlayer(InputAction.CallbackContext context)
        {
            Rotate.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        }

        /// <summary>
        /// Invoked when the RotatePlayerController action is performed with a gamepad.
        /// </summary>
        public void OnRotatePlayerController(InputAction.CallbackContext context)
        {
            RotateController.Invoke(context.ReadValue<Vector2>(), IsGamepadDevice(context));
        }

        #endregion 

        #region Button Actions

        /// <summary>
        /// Invoked when the Run action is performed.
        /// </summary>
        public void OnRun(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Run);
        }

        /// <summary>
        /// Invoked when the Jump action is performed.
        /// </summary>
        public void OnJump(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Jump);
        }

        /// <summary>
        /// Invoked when the Attack action is performed.
        /// </summary>
        public void OnAttack(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Attack);
        }

        /// <summary>
        /// Invoked when the Block action is performed.
        /// </summary>
        public void OnBlock(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Block);
        }

        /// <summary>
        /// Invoked when the Crouch action is performed.
        /// </summary>
        public void OnCrouch(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Crouch);
        }

        /// <summary>
        /// Invoked when the Interact action is performed.
        /// </summary>
        public void OnInteract(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Interact);
        }

        /// <summary>
        /// Invoked when the Escape action is performed.
        /// </summary>
        public void OnEscape(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Escape);
        }

        /// <summary>
        /// Invoked when the OpenUI action is performed.
        /// </summary>
        public void OnOpenUI(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, OpenUI);
        }

        /// <summary>
        /// Invoked when the Emote action is performed.
        /// </summary>
        public void OnEmote(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, Emote);
        }

        /// <summary>
        /// Invoked when the CommandKey action is performed.
        /// </summary>
        public void OnCommandKey(InputAction.CallbackContext context)
        {
            HandleBinaryAction(context, CommandKey);
        }

        #endregion
    }
}
