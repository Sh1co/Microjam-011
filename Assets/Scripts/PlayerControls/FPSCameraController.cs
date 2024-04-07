using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of mouse movement
    public Transform playerBody; // Reference to the player body to rotate it horizontally

    private float xRotation = 0f; // Rotation around the x-axis (for looking up and down)

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        
        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }
    }

    void Update()
    {
        if (_gc.gamePaused) return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply vertical (up and down) rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        // Apply the rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // For looking up and down
        playerBody.Rotate(Vector3.up * mouseX); // For looking left and right
    }
    
    private GlobalController _gc;
}