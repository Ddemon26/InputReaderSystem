#if UNITY_EDITOR
using InputReaderSystem.Scripts;
using UnityEditor;

namespace InputReaderSystem.Editor
{
    [CustomEditor(typeof(InputHub))]
    public class InputHubEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            InputHub inputHub = (InputHub)target;

            EditorGUILayout.LabelField("Current Input Values", EditorStyles.boldLabel);
            EditorGUILayout.Vector2Field("Move Input", inputHub.MoveInput);
            EditorGUILayout.Vector2Field("Rotate Input", inputHub.RotateInput);
            EditorGUILayout.Vector2Field("Rotate Controller Input", inputHub.RotateControllerInput);
            EditorGUILayout.Vector2Field("Scroll Wheel Input", inputHub.ScrollWheelInput);
            EditorGUILayout.Toggle("Jump Input", inputHub.JumpInput);
            EditorGUILayout.Toggle("Run Input", inputHub.RunInput);
            EditorGUILayout.Toggle("Attack Input", inputHub.AttackInput);
            EditorGUILayout.Toggle("Crouch Input", inputHub.CrouchInput);
            EditorGUILayout.Toggle("Block Input", inputHub.BlockInput);
            EditorGUILayout.Toggle("Interact Input", inputHub.InteractInput);
            EditorGUILayout.Toggle("Escape Input", inputHub.EscapeInput);
            EditorGUILayout.Toggle("Open UI Input", inputHub.OpenUIInput);
            EditorGUILayout.Toggle("Emote Input", inputHub.EmoteInput);
            EditorGUILayout.Toggle("Command Key Input", inputHub.CommandKeyInput);
            EditorGUILayout.Toggle("Num One Input", inputHub.NumOneInput);
            
            Repaint();
        }
    }
}
#endif