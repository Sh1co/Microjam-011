using UnityEngine;
using UnityEngine.SceneManagement;
public class RaycastShooting : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private string paintingTag = "Painting";
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float interactDistance = 3f;


    private void Start()
    {
        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }
    }
    private void Update()
    {
        CheckInteract();
    }

    private void CheckInteract()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        if (!_gc.slowMoActive) return;

        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        var ray = camera.ScreenPointToRay(screenCenter);

        if (!Physics.Raycast(ray, out var hit, interactDistance, layerMask)) return;
        
        if (!hit.collider.CompareTag(paintingTag)) return;
        
        var destinationScene = hit.collider.gameObject.GetComponent<DestinationScene>();
 
        SceneManager.LoadScene(destinationScene.name);

    }
    
    private GlobalController _gc;
}
