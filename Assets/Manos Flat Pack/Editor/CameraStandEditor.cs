using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraStand))]
[CanEditMultipleObjects]
public class CameraStandEditor : Editor {
    SerializedProperty glideCamera;
    SerializedProperty glideTime;

    void OnEnable() {
        glideCamera = serializedObject.FindProperty("glideCamera");
        glideTime = serializedObject.FindProperty("glideTime");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        if (GUILayout.Button("Activate")) {
            CameraStand cameraStand = ((CameraStand)target);
            if (Application.isPlaying) {
                cameraStand.PositionCamera();
            } else {
                Camera.main.transform.position = cameraStand.transform.position;
                EditorUtility.SetDirty(Camera.main.gameObject);
            }
        }

        EditorGUILayout.PropertyField(glideCamera);
        if (glideCamera.boolValue) {
            EditorGUILayout.PropertyField(glideTime);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
