using UnityEditor;

[CustomEditor(typeof(Countdown))]
[CanEditMultipleObjects]
public class CountdownEditor : Editor {
    SerializedProperty isRunning;
    SerializedProperty duration;
    SerializedProperty timeLeft;
    SerializedProperty loop;
    SerializedProperty whenComplete;

    void OnEnable() {
        isRunning = serializedObject.FindProperty("isRunning");
        duration = serializedObject.FindProperty("duration");
        timeLeft = serializedObject.FindProperty("timeLeft");
        loop = serializedObject.FindProperty("loop");
        whenComplete = serializedObject.FindProperty("whenComplete");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(isRunning);
        EditorGUILayout.PropertyField(duration);
        EditorGUILayout.PropertyField(timeLeft);
        EditorGUILayout.PropertyField(loop);
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(whenComplete);

        if (timeLeft.floatValue > duration.floatValue) {
            timeLeft.floatValue = duration.floatValue;
        }

        if (duration.floatValue < 0) {
            duration.floatValue = 0;
        }

        if (timeLeft.floatValue < 0) {
            timeLeft.floatValue = 0;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
