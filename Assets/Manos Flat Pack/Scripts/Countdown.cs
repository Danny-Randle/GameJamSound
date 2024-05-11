using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Manos/Countdown")]
public class Countdown : MonoBehaviour {
    [SerializeField] private bool isRunning = false;
    public float duration = 2;
    public float timeLeft = 2;
    public bool loop = false;
    public UnityEvent whenComplete = new UnityEvent();

    // ==================== //
    // Unity Engine Methods //
    // ==================== //

    // `Update` is run by Unity on every frame
    void Update() {
        if (isRunning) {
            timeLeft -= Time.deltaTime; // `Time.deltaTime` is the time that has passed since the last frame.

            // If the Countdown has reached zero
            if (timeLeft <= 0) { 
                
                // Trigger the `WhenComplete` event, causing all of it's effects to run
                whenComplete.Invoke();

                if (loop) {
                    // Reset the Countdown so that it loops again
                    timeLeft = duration; 
                } else {
                    // Stop the Countdown from running so that it stops
                    isRunning = false; 
                }
            }
        }
    }

    // ============== //
    // Public Methods // 
    // ============== //

    public void Play() {
        timeLeft = duration;
        isRunning = true;
    }

    public void Stop() {
        timeLeft = duration;
        isRunning = false;
    }

    public void Resume() {
        isRunning = true;
    }

    public void Pause() {
        isRunning = false;
    }

    public bool IsRunning() {
        return isRunning;
    }
}
