using UnityEngine;

[AddComponentMenu("Manos/Draggable")]
[DisallowMultipleComponent]
public class Draggable : MonoBehaviour {
    static public Draggable selected = null; // The most recently activated Draggable; is used to make sure that only one Draggable is activated at once

    public bool blockedByPhysics = false;
    public bool makeRigidBodyKinematicWhenNotDragging = true;
    public bool resetVelocityWhenDeactivated = true;

    [SerializeField] private bool _selectedOnStart = false;
    private Rigidbody2D rb;

    // 'selectedOnStart' is a specal kind of variable whose value is gotten and set according to a `get` and `set` function
    // In this case, the `set` function prevents the variable's value being changed in play mode
    // This variable is designed to be used by the `DragableEditor` script, which controls how `Draggable` components look in the inspector
    public bool selectedOnStart {
        get {
            return _selectedOnStart;
        }
        set {
            if (Application.isPlaying) {
                Debug.LogError("Cannot set Draggable.selectedOnStart during runtime.");
                return;
            }
            _selectedOnStart = value;
        }
    }

    // ==================== //
    // Unity Engine Methods //
    // ==================== //

    // `Start` is run by Unity once when the game starts
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        // Ensures that the Draggable is configured correctly once the game starts
        SetDragging(selectedOnStart); 
    }

    // `FixedUpdate` is run by Unity at a fixed framerate (this is the best place to influence the physics of a GameObject)
    void FixedUpdate() {

        // Prevent an object being dragged in the first half a second of the game
        if (Time.time < 0.5) {
            return;
        }

        bool beingDragged = selected == this;
        if (beingDragged) {
            // Convert the position of the mouse on the screen to it's corresponding position in the Unity scene
            // This is where we want the GameObject to move to
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (rb && blockedByPhysics) {
                // If the GameObject should be blocked by physics, then we need to obey the laws of physics
                // To do this, we set the velocity of the GameObject to just the right value so that it moves to the mouse in just one frame
                // `(pos - (Vector2)transform.position)` is the distance between the mouse and the GameObject's current position.
                // `Time.deltaTime` is the time that has passed since the last frame.
                rb.velocity = (pos - (Vector2)transform.position) / Time.deltaTime;

            } else {
                // If the GameObject does not need to be bloked by physics, we can just set it's position directly
                transform.position = pos;
            }
        }
    }

    // `OnDisable` is run by Unity whenever the Component or it's GameObject is disabled
    void OnDisable() {
        SetDragging(false);
    }

    // ============== //
    // Public Methods // 
    // ============== //

    public void SetDragging(bool value) {
        if (value) {
            // If there is a Draggable currently activated, deactivate it
            if (selected) {
                selected.SetDragging(false);
            }

            selected = this; // Activate this draggable

            // Make the RigidBody component kinematic (if it exists)
            if (rb && makeRigidBodyKinematicWhenNotDragging) {
                rb.isKinematic = false;
            }

        } else {
            // Check that this Draggable is active before we deactivate the selected Draggable
            if (GetDragging()) {
                selected = null;
            }

            if (rb) {
                // Reset the velocity so that the object doesn't fly away
                if (blockedByPhysics && resetVelocityWhenDeactivated) {
                    rb.velocity = Vector2.zero;
                }

                // Make the RigidBody component dynamic (if it exists)
                if (makeRigidBodyKinematicWhenNotDragging) {
                    rb.isKinematic = true;
                }
            }
        }
    }

    public void ToggleDragging() {
        SetDragging(!GetDragging()); // Set "Dragging" to the opposite of it's current value
    }

    public bool GetDragging() {
        return selected == this; // If the currently activated Draggable is this Draggable, then "dragging" is true
    }
}
