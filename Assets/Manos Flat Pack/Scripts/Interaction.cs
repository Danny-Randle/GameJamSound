using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Manos/Interaction")]
public class Interaction : MonoBehaviour {
    public enum Kind { MOUSE_LEFT, MOUSE_RIGHT, MOUSE_HOVER, OBJECT_COLLISION };
    public enum Time { BEGIN, END };

    public Kind kind = Kind.MOUSE_LEFT;
    public Time time = Time.BEGIN;
    public string objectTag;
    public UnityEvent whenInteracted = new UnityEvent();

    private Collider2D[] colliders; // Stores all of the Collider2D components that are attached to the GameObject
    private ContactFilter2D contactFilter = new ContactFilter2D();
    private bool wasColliding = false; // Wheter or not this object was colliding with another object on the previous frame

    // ==================== //
    // Unity Engine Methods //
    // ==================== //

    // `Start` is run by Unity once when the game starts
    void Start() {
        colliders = GetComponents<Collider2D>();
        contactFilter.NoFilter();
    }

    // `Update` is run by Unity on every frame
    void Update() {
        if (kind == Kind.OBJECT_COLLISION) {
            bool isColliding = TouchingCollisionTag();
            if (wasColliding != isColliding) {
                wasColliding = isColliding;
                if (isColliding && time == Time.BEGIN || !isColliding && time == Time.END) {
                    whenInteracted.Invoke();
                }
            }
        }
    }

    // `OnMouseOver` is run by Unity on every frame where the mouse is over one of the GameObject's colliders
    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            InvokeIf(Kind.MOUSE_LEFT, Time.BEGIN);
        }
        if (Input.GetMouseButtonUp(0)) {
            InvokeIf(Kind.MOUSE_LEFT, Time.END);
        }

        if (Input.GetMouseButtonDown(1)) {
            InvokeIf(Kind.MOUSE_RIGHT, Time.BEGIN);
        }
        if (Input.GetMouseButtonUp(1)) {
            InvokeIf(Kind.MOUSE_RIGHT, Time.END);
        }
    }

    // `OnMouseEnter` is run once by Unity when the mouse starts colliding with any of the GameObject's colliders
    void OnMouseEnter() {
        InvokeIf(Kind.MOUSE_HOVER, Time.BEGIN);
    }

    // `OnMouseExit` is run once by Unity when the mouse stops colliding with any of the GameObject's colliders
    void OnMouseExit() {
        InvokeIf(Kind.MOUSE_HOVER, Time.END);
    }

    // ======================= //
    // Private Utility Methods //
    // ======================= //

    // `InvokeIf` will invoke the `WhenInteracted` only if the correct Type and Time are given
    private void InvokeIf(Kind k, Time t) {
        if (kind == k && time == t) {
            whenInteracted.Invoke();
        }
    }

    // `TouchingCollisionTag` checks if any of the GameObject's colliders are colliding with another GameObject with the correct tag
    private bool TouchingCollisionTag() {
        List<Collider2D> results = new List<Collider2D>();
        foreach (var collider in colliders) { // For each of the GameObject's colliders
            collider.OverlapCollider(contactFilter, results); // See which colliders it is colliding with and store the list in `results`
            foreach (var other in results) { // For each result
                bool colliderBelongsToMe = other.gameObject == gameObject;
                bool correctTag = other.gameObject.tag == objectTag;
                if (correctTag && !colliderBelongsToMe) {
                    // Return true, as we are colliding with a GameObject with the correct tag
                    return true;
                }
            }
        }
        // We did not collider with another GameObject with the correct tag, so return false
        return false;
    }
}
