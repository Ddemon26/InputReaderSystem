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
        [Header("Input Settings")]
        [SerializeField] private bool canReceiveInput = true;
        public bool CanReceiveInput() => canReceiveInput;
        [SerializeField] private bool isMouseConnected = true;
        public bool IsMouseConnected() => isMouseConnected;
        [SerializeField] private bool isGamepadConnected;
        public bool IsGamepadConnected() => isGamepadConnected;
        [SerializeField] private bool invertY = true;
        public bool InvertY() => invertY;
        [SerializeField] private bool invertX;
        public bool InvertX() => invertX;
        [Header("Control Scheme")]
        private ControlScheme currentControlScheme;
        public ControlScheme CurrentControlScheme() => currentControlScheme;
        [Header("Input Values")]
        [SerializeField] private Vector2 moveInput = Vector2.zero;
        public Vector2 MoveInput() => moveInput;
        [SerializeField] private Vector2 rotateInput = Vector2.zero;
        public Vector2 RotateInput() => rotateInput;
        [SerializeField] private bool analogMovement;
        public bool AnalogMovement() => analogMovement;
        [SerializeField] private Vector2 rotateControllerInput = Vector2.zero;
        public Vector2 RotateControllerInput() => rotateControllerInput;
        [SerializeField] private Vector2 scrollWheelInput;
        public Vector2 ScrollWheelInput() => scrollWheelInput;
        [Header("Input Actions")]
        [SerializeField] private bool jumpInput;
        public bool JumpInput() => jumpInput;
        [SerializeField] private bool runInput;
        public bool RunInput() => runInput;
        [SerializeField] private bool reloadInput;
        public bool ReloadInput() => reloadInput;
        [SerializeField] private bool attackInput;
        public bool AttackInput() => attackInput;
        [SerializeField] private bool crouchInput;
        public bool CrouchInput() => crouchInput;
        [SerializeField] private bool blockInput;
        public bool BlockInput() => blockInput;
        [SerializeField] private bool interactInput;
        public bool InteractInput() => interactInput;
        [SerializeField] private bool escapeInput;
        public bool EscapeInput() => escapeInput;
        [SerializeField] private bool openUIInput;
        public bool OpenUIInput() => openUIInput;
        [SerializeField] private bool emoteInput;
        public bool EmoteInput() => emoteInput;
        [SerializeField] private bool commandKeyInput;
        public bool CommandKeyInput() => commandKeyInput;
        [SerializeField] private bool numOneInput;
        public bool NumOneInput() => numOneInput;

        private void OnEnable()
        {
            try
            {
                inputReader = ScriptableObject.CreateInstance<InputReader>();

                if (!canReceiveInput) return;
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
            inputReader.Reload += OnReload;
            inputReader.Attack += OnAttack;
            inputReader.Crouch += OnCrouch;
            inputReader.Block += OnBlock;
            inputReader.Interact += OnInteract;
            inputReader.Escape += OnEscape;
            inputReader.OpenUI += OnOpenUI;
            inputReader.Emote += OnEmote;
            inputReader.CommandKey += OnCommandKey;
            inputReader.NumOne += OnNumOne;

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
            inputReader.Reload -= OnReload;
            inputReader.Attack -= OnAttack;
            inputReader.Crouch -= OnCrouch;
            inputReader.Block -= OnBlock;
            inputReader.Interact -= OnInteract;
            inputReader.Escape -= OnEscape;
            inputReader.OpenUI -= OnOpenUI;
            inputReader.Emote -= OnEmote;
            inputReader.CommandKey -= OnCommandKey;
            inputReader.NumOne -= OnNumOne;

            inputReader.DisablePlayerActions();
        }

        public void SetCanReceiveInput(bool value)
        {
            canReceiveInput = value;
            if (canReceiveInput)
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
            currentControlScheme = controlScheme;
            inputReader.EnablePlayerActions();
        }

        private void CheckConnectedDevices()
        {
            isMouseConnected = UnityEngine.InputSystem.Mouse.current != null;
            isGamepadConnected = Input.GetJoystickNames().Length > 0;
        }

        public void InvertYAxis(bool value)
        {
            invertY = value;
        }

        public void InvertXAxis(bool value)
        {
            invertX = value;
        }

        private static void HandleBooleanInput(bool input, Action<bool> setInputAction) => setInputAction(input);

        private void OnMove(Vector2 input)
        {
            moveInput = input;
        }

        private void OnRotate(Vector2 input, bool isMouseDevice)
        {
            if (!isMouseDevice) return;
            ProcessInput(ref input);
            rotateInput = input;
        }

        private void OnRotateController(Vector2 input, bool isGamepadDevice)
        {
            if (!isGamepadDevice) return;
            ProcessInput(ref input);
            rotateControllerInput = input;
        }

        private void OnScrollWheel(Vector2 value, bool isMouseDevice)
        {
            scrollWheelInput = value;
        }

        private void ProcessInput(ref Vector2 input)
        {
            if (invertY)
            {
                input.y = -input.y;
            }

            if (invertX)
            {
                input.x = -input.x;
            }

            rotateInput = new Vector2(input.x, input.y);
        }

        private void OnJump(bool isPressed) => HandleBooleanInput(isPressed, value => jumpInput = value);
        private void OnRun(bool isPressed) => HandleBooleanInput(isPressed, value => runInput = value);
        private void OnReload(bool isPressed) => HandleBooleanInput(isPressed, value => reloadInput = value);
        private void OnAttack(bool isPressed) => HandleBooleanInput(isPressed, value => attackInput = value);
        private void OnCrouch(bool isPressed) => HandleBooleanInput(isPressed, value => crouchInput = value);
        private void OnBlock(bool isPressed) => HandleBooleanInput(isPressed, value => blockInput = value);
        private void OnInteract(bool isPressed) => HandleBooleanInput(isPressed, value => interactInput = value);
        private void OnEscape(bool isPressed) => HandleBooleanInput(isPressed, value => escapeInput = value);
        private void OnOpenUI(bool isPressed) => HandleBooleanInput(isPressed, value => openUIInput = value);
        private void OnEmote(bool isPressed) => HandleBooleanInput(isPressed, value => emoteInput = value);
        private void OnCommandKey(bool isPressed) => HandleBooleanInput(isPressed, value => commandKeyInput = value);
        private void OnNumOne(bool isPressed) => HandleBooleanInput(isPressed, value => numOneInput = value);
    }
}