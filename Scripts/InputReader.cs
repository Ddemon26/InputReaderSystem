using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InputReaderSystem.Scripts
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "PlayerController/InputReader")]
    public class InputReader : ScriptableObject, PlayerInput.ICharacterControlsActions
    {
        private PlayerInput inputActions;

        public event UnityAction<Vector2> Move = delegate { };
        public Vector3 Direction => inputActions.CharacterControls.Move.ReadValue<Vector2>();
        public event UnityAction<Vector2, bool> Rotate = delegate { };
        public event UnityAction<Vector2, bool> RotateController = delegate { };
        public event UnityAction<Vector2, bool> ScrollWheel = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Run = delegate { };
        public event UnityAction<bool> Reload = delegate { };
        public event UnityAction<bool> Attack = delegate { };
        public event UnityAction<bool> Crouch = delegate { };
        public event UnityAction<bool> Block = delegate { };
        public event UnityAction<bool> Interact = delegate { };
        public event UnityAction<bool> Escape = delegate { };
        public event UnityAction<bool> OpenUI = delegate { };
        public event UnityAction<bool> Emote = delegate { };
        public event UnityAction<bool> CommandKey = delegate { };
        public event UnityAction<bool> NumOne = delegate { };

        private void OnEnable()
        {
            if (inputActions != null) return;

            inputActions = new PlayerInput();
            inputActions.CharacterControls.SetCallbacks(this);
        }

        public void EnablePlayerActions() => inputActions.Enable();
        public void DisablePlayerActions() => inputActions.Disable();

        private static bool IsMouseDevice(InputAction.CallbackContext context) =>
            context.control.device.name == "Mouse";
        private static bool IsGamepadDevice(InputAction.CallbackContext context) =>
            context.control.device.name.Contains("Gamepad");
        public void OnMove(InputAction.CallbackContext context) => Move.Invoke(context.ReadValue<Vector2>());
        public void OnScrollWheel(InputAction.CallbackContext context) =>
            ScrollWheel.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        public void OnRotatePlayer(InputAction.CallbackContext context) =>
            Rotate.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        public void OnRotatePlayerController(InputAction.CallbackContext context) =>
            RotateController.Invoke(context.ReadValue<Vector2>(), IsGamepadDevice(context));
        private static void HandleBinaryAction(InputAction.CallbackContext context, UnityAction<bool> action)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                case InputActionPhase.Performed:
                    action.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                case InputActionPhase.Waiting:
                case InputActionPhase.Disabled:
                    action.Invoke(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void OnRun(InputAction.CallbackContext context) => HandleBinaryAction(context, Run);
        public void OnReload(InputAction.CallbackContext context) => HandleBinaryAction(context, Reload);
        public void OnJump(InputAction.CallbackContext context) => HandleBinaryAction(context, Jump);
        public void OnCrouch(InputAction.CallbackContext context) => HandleBinaryAction(context, Crouch);
        public void OnInteract(InputAction.CallbackContext context) => HandleBinaryAction(context, Interact);
        public void OnEscape(InputAction.CallbackContext context) => HandleBinaryAction(context, Escape);
        public void OnOpenUI(InputAction.CallbackContext context) => HandleBinaryAction(context, OpenUI);
        public void OnEmote(InputAction.CallbackContext context) => HandleBinaryAction(context, Emote);
        public void OnCommandKey(InputAction.CallbackContext context) => HandleBinaryAction(context, CommandKey);
        public void OnNumOne(InputAction.CallbackContext context) => HandleBinaryAction(context, NumOne);

        private static void HandleHoldAction(InputAction.CallbackContext context, UnityAction<bool> action)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                case InputActionPhase.Performed:
                case InputActionPhase.Waiting:
                    action.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                case InputActionPhase.Disabled:
                    action.Invoke(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnAttack(InputAction.CallbackContext context) => HandleHoldAction(context, Attack);
        public void OnBlock(InputAction.CallbackContext context) => HandleHoldAction(context, Block);
    }
}