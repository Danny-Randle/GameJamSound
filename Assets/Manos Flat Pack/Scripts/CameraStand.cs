using UnityEngine;

[AddComponentMenu("Manos/Camera Stand")]
[DisallowMultipleComponent]
public class CameraStand : MonoBehaviour {
    public static CameraStand activeStand; // The most recently activated CameraStand; Is used to make sure that only the most recently activated CameraStand is moving the camera at any given time

    public bool glideCamera = true; // Whether or not the camera should glide to this CameraStand when it is activated
    public float glideTime = 0.8f; // The length of the glide animation

    private float animationPlayback; // How far through the glide animation the camera is
    private Vector3 cameraStart; // The starting position of the glide animation
    private Vector3 cameraEnd; // The ending position of the glide animation

    // ==================== //
    // Unity Engine Methods //
    // ==================== //

    // `Update` is run by Unity on every frame
    void Update() {
        bool animatingCamera = activeStand == this;
        bool animationIncomplete = animationPlayback < glideTime;        
        if (glideCamera && animatingCamera && animationIncomplete) {
            animationPlayback += Time.deltaTime; // `Time.deltaTime` is the time that has passed since the last frame.
            animationPlayback = Mathf.Clamp(animationPlayback, 0, glideTime);
            Camera.main.transform.position = SmoothInterpolate(cameraStart, cameraEnd, animationPlayback / glideTime);
        }
    }

    // ==================== //
    // Unity Editor Methods //
    // ==================== //

    // `OnDrawGizmos` is where the blue box showing the camera area is drawn
    void OnDrawGizmos() {
        Gizmos.color = new Color(0, .5f, .5f, .5f);
        DrawCameraGizmo();
    }

    // `OnDrawGizmosSelected` is where the yellow box showing the camera area is drawn when the GameObject is selected
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        DrawCameraGizmo();
    }

    // ============== //
    // Public Methods // 
    // ============== //

    // `PositionCamera` is used to move the camera GameObject to the position of the CameraStand
    public void PositionCamera() {
        activeStand = this;
        if (glideCamera) {
            cameraStart = Camera.main.transform.position;
            cameraEnd = transform.position; // Set the end position to the position of the CameraStand
            cameraEnd.z = cameraStart.z; // Stops the camera moving in the z direction
            animationPlayback = 0;
        } else {
            Camera.main.transform.position = transform.position;
        }
    }

    // ======================= //
    // Private Utility Methods //
    // ======================= //

    // `SmoothInterpolate` uses a cosine wave to smoothly interpolate between two vectors.
    private Vector3 SmoothInterpolate(Vector3 a, Vector3 b, float t) {
        t = Mathf.Clamp(t, 0, 1);
        float s = (Mathf.Cos(t * Mathf.PI) + 1) / 2;
        return a * s + b * (1 - s); // Interpolate between a and b according to t
    }

    // `DrawCameraGizmo` is a utily function used by `OnDrawGizmos` and `OnDrawGizmosSelected`
    private void DrawCameraGizmo() {
        Camera cam = Camera.main;
        float vert = cam.orthographicSize * 2;
        float hori = vert * cam.aspect;
        Gizmos.DrawWireCube(transform.position, new Vector3(hori, vert, 1));
    }
}
