using TMPro;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    [SerializeField] private GameObject bookHUD;
    // [SerializeField] private List<GameObject> pages;

    private void Start()
    {

        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }

        pages = bookHUD.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {

            bookHUD.SetActive(!bookHUD.activeSelf);
            _gc.gamePaused = !_gc.gamePaused;

            if (_gc.gamePaused) Time.timeScale = 0f;
            else Time.timeScale = 1;

        }

        if (!_gc.gamePaused) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPage < pages.Length - 1)
            {
                pages[currentPage].gameObject.SetActive(false);
                currentPage++;
                pages[currentPage].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPage > 0)
            {
                pages[currentPage].gameObject.SetActive(false);
                currentPage--;
                pages[currentPage].gameObject.SetActive(true);
            }
        }
    }

    private GlobalController _gc;
    private int currentPage = 0;
    private TextMeshProUGUI[] pages;
}
