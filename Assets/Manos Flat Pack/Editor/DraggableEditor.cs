using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Draggable))]
[CanEditMultipleObjects]
public class DraggableEditor : Editor {
    SerializedProperty blockedByPhysics;
    SerializedProperty makeRigidBodyKinematicWhenNotDragging;
    SerializedProperty resetVelocityWhenDeactivated;
    SerializedProperty selectedOnStart;

    private string[] toggleBarString = { "Off", "On" };

    void OnEnable() {
        blockedByPhysics = serializedObject.FindProperty("blockedByPhysics");
        makeRigidBodyKinematicWhenNotDragging = serializedObject.FindProperty("makeRigidBodyKinematicWhenNotDragging");
        resetVelocityWhenDeactivated = serializedObject.FindProperty("resetVelocityWhenDeactivated");
        selectedOnStart = serializedObject.FindProperty("_selectedOnStart");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        Draggable draggable = (Draggable)target;

        bool wasSelected = Application.isPlaying ? draggable == Draggable.selected : selectedOnStart.boolValue;
        bool isSelected = GUILayout.Toolbar(wasSelected ? 1 : 0, toggleBarString) > 0;

        if (wasSelected != isSelected) {
            if (Application.isPlaying) {
                draggable.SetDragging(isSelected);
            } else {
                foreach (Draggable other in Resources.FindObjectsOfTypeAll<Draggable>()) {
                    other.selectedOnStart = false;
                    EditorUtility.SetDirty(other.gameObject);
                }
                selectedOnStart.boolValue = isSelected;
            }
        }

        EditorGUILayout.PropertyField(blockedByPhysics);

        bool hasRb = draggable.GetComponent<Rigidbody2D>() != null;
        bool hasCollider = draggable.GetComponent<Collider2D>() != null;
        if (blockedByPhysics.boolValue && !(hasRb && hasCollider)) {
            string msg = "In order for this draggable GameObject to be blocked by physics it must have a RigidBody2D and a 2D Collider.";
            if (draggable.GetComponent<Rigidbody>()) {
                msg += " (It looks like you have accidentally added a 3D RigidBody.)";
            } else if (draggable.GetComponent<Collider>()) {
                msg += " (It looks like you have accidentally added a 3D Collider.)";
            }
            EditorGUILayout.HelpBox(msg, MessageType.Warning, true);
        }

        if (hasRb) {
            EditorGUILayout.PropertyField(makeRigidBodyKinematicWhenNotDragging);
            if (blockedByPhysics.boolValue) {
                EditorGUILayout.PropertyField(resetVelocityWhenDeactivated);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
