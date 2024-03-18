# Enhance Your Game with the InputReader System

The InputReader system is a cutting-edge input handling solution designed for Unity developers seeking a streamlined and powerful way to manage player inputs. This system is a key component of the Scriptable Object Architecture package, providing a flexible and efficient approach to input management that can elevate your game to new heights.

## Why Choose InputReader?

- **Seamless Integration**: As a ScriptableObject, InputReader can be easily integrated into any Unity project, allowing for quick setup and instant improvements to your input handling.

- **Customizable and Extensible**: Define your own input actions and tailor the system to fit your game's unique requirements. The InputReader is designed to grow with your project, ensuring that your input handling evolves alongside your game.

- **Device Agnostic**: Whether your players prefer a keyboard and mouse or a gamepad, the InputReader seamlessly accommodates various input devices, providing a consistent and enjoyable experience for all.

- **Event-Driven Architecture**: Say goodbye to cluttered input checks in your update loops. The InputReader raises events for specific input actions, keeping your code clean, organized, and easy to maintain.

- **Responsive and Accurate**: Built with performance in mind, the InputReader ensures that every input is captured and processed with precision, delivering a responsive and satisfying player experience.

## Key Features:

- **Movement and Rotation**: Capture smooth and intuitive player movements and rotations with easy-to-use events.

- **Action Inputs**: Jump, run, attack, and more - the InputReader supports a wide range of action inputs, each with customizable events.

- **User Interface Interaction**: Seamlessly integrate input handling for in-game UI, including opening menus and interacting with elements.

- **Advanced Control Options**: Support for scrolling, crouching, blocking, and other advanced controls, providing a comprehensive input solution for your game.

## Unlock the Potential of Your Game with InputReader

The InputReader system is more than just an input handler; it's a gateway to creating more immersive and interactive gameplay experiences. By choosing InputReader, you're not just enhancing your game's input handling - you're investing in a tool that adapts to your needs, simplifies your development process, and delivers a superior gaming experience to your players.

Elevate your game with the InputReader system - the ultimate input solution for Unity developers.

***

Here's a high-level example script that demonstrates how to use the InputReader system in a Unity project:

```csharp
using UnityEngine;
using ScriptableArchitect.InputSettings;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader; // Reference to the InputReader ScriptableObject

    private Vector2 moveInput;

    private void OnEnable()
    {
        // Subscribe to input events
        inputReader.Move += OnMove;
        inputReader.Jump += OnJump;
        inputReader.Run += OnRun;
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        inputReader.Move -= OnMove;
        inputReader.Jump -= OnJump;
        inputReader.Run -= OnRun;
    }

    private void OnMove(Vector2 input)
    {
        moveInput = input;
        // Move the player based on input
        // Example: transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    private void OnJump(bool isJumping)
    {
        if (isJumping)
        {
            // Trigger jump logic
            // Example: rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnRun(bool isRunning)
    {
        // Adjust movement speed based on running state
        // Example: moveSpeed = isRunning ? runSpeed : walkSpeed;
    }

    // Additional input event handlers can be added here, such as OnAttack, OnCrouch, etc.

    private void Update()
    {
        // Update logic that is not directly tied to input events
        // Example: Animation updates, physics checks, etc.
    }
}
```

In this example, the `PlayerController` script subscribes to the `Move`, `Jump`, and `Run` events from the `InputReader` ScriptableObject. When these input events are triggered, the corresponding event handlers (`OnMove`, `OnJump`, `OnRun`) are called, allowing you to implement the specific logic for each action, such as moving the player, making them jump, or toggling their running state.

You can easily extend this script by adding more event handlers for other input actions defined in your `InputReader` ScriptableObject, enabling you to create a comprehensive and responsive control system for your game.
