using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interaction))]
[CanEditMultipleObjects]
public class InteractionEditor : Editor {
    SerializedProperty kind;
    SerializedProperty time;
    SerializedProperty objectTag;
    SerializedProperty whenInteracted;

    void OnEnable() {
        kind = serializedObject.FindProperty("kind");
        time = serializedObject.FindProperty("time");
        objectTag = serializedObject.FindProperty("objectTag");
        whenInteracted = serializedObject.FindProperty("whenInteracted");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        Interaction interaction = (Interaction)target;

        EditorGUILayout.PropertyField(kind);
        EditorGUILayout.PropertyField(time);
        if (kind.enumValueIndex == (int)Interaction.Kind.OBJECT_COLLISION) {
            EditorGUILayout.PropertyField(objectTag);
        }

        if (!interaction.GetComponent<Collider2D>()) {
            string msg = "In order for this GameObject to be interactable it must have a 2D Collider.";
            if (interaction.GetComponent<Collider>()) {
                msg += " (It looks like you have accidentally added a 3D Collider.)";
            }
            EditorGUILayout.HelpBox(msg, MessageType.Warning, true);
        }

        GUILayout.Space(10);
        EditorGUILayout.PropertyField(whenInteracted);

        serializedObject.ApplyModifiedProperties();
    }
}
