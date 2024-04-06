using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float sprintSpeedMultiplier = 2f; // Speed while sprinting
    public float gravity = -9.8f;
    public float jumpHeight = 2f; // Height of the jump
    public float crouchHeight = 0.5f; // Height of the character when crouching
    public float standHeight = 2.0f; // Normal standing height
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0); // Center of the character controller while crouching
    public Vector3 standCenter = new Vector3(0, 0, 0); // Center of the character controller while standing
    
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isCrouching = false;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        
        float currentSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintSpeedMultiplier;
        }
        
        // Check for crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }

        movement = transform.TransformDirection(movement) * currentSpeed;
        
        _characterController.Move(movement * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && _isGrounded && !_isCrouching)
        {
            _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    void StartCrouch()
    {
        _characterController.height = crouchHeight;
        _characterController.center = crouchCenter;
        _isCrouching = true;
    }

    void StopCrouch()
    {
        _characterController.height = standHeight;
        _characterController.center = standCenter;
        _isCrouching = false;
    }
}
