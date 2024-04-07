using UnityEngine;
using UnityEngine.SceneManagement;
public class RaycastShooting : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private string paintingTag = "Painting";
    
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        CheckInteract();
    }

    private void CheckInteract()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        var ray = camera.ScreenPointToRay(screenCenter);

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask)) return;
        
        if (!hit.collider.CompareTag(paintingTag)) return;
        
        var destinationScene = hit.collider.gameObject.GetComponent<DestinationScene>();
 
        SceneManager.LoadScene(destinationScene.name);

    }
}
