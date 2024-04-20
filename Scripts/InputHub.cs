using System;
using UnityEngine;

namespace InputReaderSystem.Scripts
{
    public enum ControlScheme
    {
        KeyboardMouse,
        Gamepad,
        Mobile
    }

    public class InputHub : MonoBehaviour
    {
        private InputReader inputReader;
        public bool CanReceiveInput { get; private set; } = true;
        public bool IsMouseConnected { get; private set; } = true;
        public bool IsGamepadConnected { get; private set; }

        public bool InvertY { get; private set; } = true;
        public bool InvertX { get; private set; }

        public ControlScheme CurrentControlScheme { get; private set; }

        public Vector2 MoveInput { get; private set; } = Vector2.zero;
        public Vector2 RotateInput { get; private set; } = Vector2.zero;
        public bool AnalogMovement { get; private set; } = false;
        public Vector2 RotateControllerInput { get; private set; } = Vector2.zero;

        public float ScrollWheelInput { get; private set; }

        public bool JumpInput { get; set; }
        public bool RunInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool CrouchInput { get; private set; }
        public bool BlockInput { get; private set; }
        public bool InteractInput { get; private set; }
        public bool EscapeInput { get; private set; }
        public bool OpenUIInput { get; private set; }
        public bool EmoteInput { get; private set; }
        public bool CommandKeyInput { get; private set; }

        private void OnEnable()
        {
            try
            {
                inputReader = ScriptableObject.CreateInstance<InputReader>();

                if (!CanReceiveInput) return;
                CheckConnectedDevices();
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex.Message}");
            }

            inputReader.Move += OnMove;
            inputReader.Rotate += OnRotate;
            inputReader.RotateController += OnRotateController;
            inputReader.ScrollWheel += OnScrollWheel;
            inputReader.Jump += OnJump;
            inputReader.Run += OnRun;
            inputReader.Attack += OnAttack;
            inputReader.Crouch += OnCrouch;
            inputReader.Block += OnBlock;
            inputReader.Interact += OnInteract;
            inputReader.Escape += OnEscape;
            inputReader.OpenUI += OnOpenUI;
            inputReader.Emote += OnEmote;
            inputReader.CommandKey += OnCommandKey;

            inputReader.EnablePlayerActions();
        }

        private void OnDisable()
        {
            inputReader.Move -= OnMove;
            inputReader.Rotate -= OnRotate;
            inputReader.RotateController -= OnRotateController;
            inputReader.ScrollWheel -= OnScrollWheel;
            inputReader.Jump -= OnJump;
            inputReader.Run -= OnRun;
            inputReader.Attack -= OnAttack;
            inputReader.Crouch -= OnCrouch;
            inputReader.Block -= OnBlock;
            inputReader.Interact -= OnInteract;
            inputReader.Escape -= OnEscape;
            inputReader.OpenUI -= OnOpenUI;
            inputReader.Emote -= OnEmote;
            inputReader.CommandKey -= OnCommandKey;

            inputReader.DisablePlayerActions();
        }

        public void SetCanReceiveInput(bool value)
        {
            CanReceiveInput = value;
            if (CanReceiveInput)
            {
                inputReader.EnablePlayerActions();
            }
            else
            {
                inputReader.DisablePlayerActions();
            }
        }

        public void ChangeControlScheme(ControlScheme controlScheme)
        {
            CurrentControlScheme = controlScheme;
            inputReader.EnablePlayerActions();
        }

        private void CheckConnectedDevices()
        {
            IsMouseConnected = Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
            IsGamepadConnected = Input.GetJoystickNames().Length > 0;
        }

        public void InvertYAxis(bool value)
        {
            InvertY = value;
        }

        public void InvertXAxis(bool value)
        {
            InvertX = value;
        }

        private static void HandleBooleanInput(bool input, Action<bool> setInputAction) => setInputAction(input);

        private void OnMove(Vector2 input)
        {
            MoveInput = input;
        }

        private void OnRotate(Vector2 input, bool isMouseDevice)
        {
            if (!isMouseDevice) return;
            ProcessInput(ref input);
            RotateInput = input;
        }

        private void OnRotateController(Vector2 input, bool isGamepadDevice)
        {
            if (!isGamepadDevice) return;
            ProcessInput(ref input);
            RotateControllerInput = input;
        }

        private void ProcessInput(ref Vector2 input)
        {
            if (InvertY)
            {
                input.y = -input.y;
            }

            if (InvertX)
            {
                input.x = -input.x;
            }

            RotateInput = new Vector2(input.x, input.y);
        }

        private void OnScrollWheel(float value) => ScrollWheelInput = value;
        private void OnJump(bool isPressed) => HandleBooleanInput(isPressed, value => JumpInput = value);
        private void OnRun(bool isPressed) => HandleBooleanInput(isPressed, value => RunInput = value);
        private void OnAttack(bool isPressed) => HandleBooleanInput(isPressed, value => AttackInput = value);
        private void OnCrouch(bool isPressed) => HandleBooleanInput(isPressed, value => CrouchInput = value);
        private void OnBlock(bool isPressed) => HandleBooleanInput(isPressed, value => BlockInput = value);
        private void OnInteract(bool isPressed) => HandleBooleanInput(isPressed, value => InteractInput = value);
        private void OnEscape(bool isPressed) => HandleBooleanInput(isPressed, value => EscapeInput = value);
        private void OnOpenUI(bool isPressed) => HandleBooleanInput(isPressed, value => OpenUIInput = value);
        private void OnEmote(bool isPressed) => HandleBooleanInput(isPressed, value => EmoteInput = value);
        private void OnCommandKey(bool isPressed) => HandleBooleanInput(isPressed, value => CommandKeyInput = value);
    }
}