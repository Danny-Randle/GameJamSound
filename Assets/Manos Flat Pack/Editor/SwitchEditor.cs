using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Switch))]
[CanEditMultipleObjects]
public class SwitchEditor : Editor {
    SerializedProperty switched;
    SerializedProperty whenActivated;
    SerializedProperty whenDeactivated;

    private string[] toggleBarString = { "Off", "On" };

    void OnEnable() {
        switched = serializedObject.FindProperty("switched");
        whenActivated = serializedObject.FindProperty("whenActivated");
        whenDeactivated = serializedObject.FindProperty("whenDeactivated");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        switched.boolValue = GUILayout.Toolbar(switched.boolValue ? 1 : 0, toggleBarString) > 0;
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(whenActivated);
        EditorGUILayout.PropertyField(whenDeactivated);

        serializedObject.ApplyModifiedProperties();
    }
}
