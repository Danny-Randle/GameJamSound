using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Manos/Switch")]
public class Switch : MonoBehaviour {
    [SerializeField] private bool switched;
    public UnityEvent whenActivated = new UnityEvent();
    public UnityEvent whenDeactivated = new UnityEvent();

    // ============== //
    // Public Methods // 
    // ============== //

    public bool GetSwitched() {
        return switched;
    }

    public void SetSwtiched(bool value) {
        // If we are already in the right state, return from the function before doing anything
        if (switched == value) {
            return;
        }

        // Switch to the new state and (if we are in play mode) invoke the corresponding event
        switched = value;
        if (Application.isPlaying) {
            (switched ? whenActivated : whenDeactivated).Invoke();
        }
    }

    public void ToggleSwitched() {
        SetSwtiched(!switched); // Set `switched` to the opposite of it's current value
    }
}
