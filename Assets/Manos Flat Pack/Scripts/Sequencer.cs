using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Manos/Sequencer")]
public class Sequencer : MonoBehaviour {
    [SerializeField] private int currentStep = 0;
    public List<UnityEvent> steps = new List<UnityEvent>();
    public bool loops = false;

    // ============== //
    // Public Methods // 
    // ============== //

    public int GetStep() {
        return currentStep;
    }

    public void SetStep(int step) {
        // If we are already on the right step, return from the function before doing anything
        if (currentStep == step) {
            return;
        }

        // If the step is invalid, throw an error
        if (step < 0) {
            Debug.LogError(string.Format("Attempt to set a Sequencer on \"{0}\" to step {1}.", gameObject.name, step));
            return;
        }
        if (step >= steps.Count) {
            Debug.LogError(string.Format("Attempt to set a Sequencer on \"{0}\" to step {1}. There are only {2} steps on that Sequencer. (Remember that in programming, we start counting from 0, so step {3} is the last step).", gameObject.name, step, steps.Count, steps.Count - 1));
            return;
        }

        // Switch to the new step and (if we are in play mode) invoke the corresponding event
        currentStep = step;
        if (Application.isPlaying) {
            steps[step].Invoke();
        }
    }

    public void NextStep() {
        int step = currentStep + 1;
        if (step >= steps.Count) {
            step = loops ? 0 : steps.Count - 1;
        }

        SetStep(step);
    }

    public void PrevStep() {
        int step = currentStep - 1;
        if (step < 0) {
            step = loops ? steps.Count - 1 : 0;
        }

        SetStep(step);
    }

    public void FirstStep() {
        SetStep(0);
    }

    public void LastStep() {
        SetStep(steps.Count - 1);
    }
}
