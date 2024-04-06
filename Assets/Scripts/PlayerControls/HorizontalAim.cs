using UnityEngine;

public class HorizontalAim : MonoBehaviour
{

    public float sens = 1f;
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void FixedUpdate()
    {
        float mouseInput = Input.GetAxis("Mouse X") * sens;

        Vector3 rot = new Vector3(0, mouseInput, 0) * Time.fixedDeltaTime;
        
        transform.Rotate(rot);
    }
}
