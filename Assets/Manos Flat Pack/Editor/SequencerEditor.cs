using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sequencer))]
[CanEditMultipleObjects]
public class SequencerEditor : Editor {
    SerializedProperty steps;
    SerializedProperty loops;

    void OnEnable() {
        steps = serializedObject.FindProperty("steps");
        loops = serializedObject.FindProperty("loops");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        Sequencer sequencer = (Sequencer)serializedObject.targetObject;
        int currentStep = sequencer.GetStep();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<")) {
            sequencer.FirstStep();
            EditorUtility.SetDirty(sequencer);
        }

        if (GUILayout.Button("<")) {
            sequencer.PrevStep();
            EditorUtility.SetDirty(sequencer);
        }

        int newStep = EditorGUILayout.IntField(currentStep);
        if (newStep != currentStep) {
            sequencer.SetStep(newStep);
            EditorUtility.SetDirty(sequencer);
        }

        if (GUILayout.Button(">")) {
            sequencer.NextStep();
            EditorUtility.SetDirty(sequencer);
        }

        if (GUILayout.Button(">>")) {
            sequencer.LastStep();
            EditorUtility.SetDirty(sequencer);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(loops);
        EditorGUILayout.PropertyField(steps);

        serializedObject.ApplyModifiedProperties();
    }
}
