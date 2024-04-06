using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAim : MonoBehaviour
{
    public float sens = 1f;
    public float maxRot = 30f;
    public float minRot = -30f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseInput = Input.GetAxis("Mouse Y") * sens;

        _verticalRot -= mouseInput * Time.fixedDeltaTime;
        _verticalRot = Mathf.Clamp(_verticalRot, minRot, maxRot);

        transform.localEulerAngles = new Vector3(_verticalRot, 0, 0);
    }

    private float _verticalRot = 0;
}
