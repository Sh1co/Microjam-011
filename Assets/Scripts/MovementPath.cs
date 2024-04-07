using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public Transform[] points;
    
    private void Awake()
    {
        Transform[] allTransforms = gameObject.GetComponentsInChildren<Transform>();

        // Check if there are any children; if the array only contains the parent's transform, do nothing
        if (allTransforms.Length > 1)
        {
            // Create a new array that excludes the first item (the parent itself)
            points = new Transform[allTransforms.Length - 1];
            for (int i = 1; i < allTransforms.Length; i++)
            {
                points[i - 1] = allTransforms[i];
            }
        }
        else
        {
            // No children, so just initialize points to an empty array to avoid null references
            points = new Transform[0];
        }
    }

}