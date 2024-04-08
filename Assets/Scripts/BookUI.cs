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

        var pages1 = bookHUD.GetComponentsInChildren<RectTransform>();
        // remove the first page, which is the book cover
        pages = new RectTransform[pages1.Length - 1];
        for (int i = 1; i < pages1.Length; i++)
        {
            pages[i - 1] = pages1[i];
        }


        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
        // bookHUD.SetActive(!bookHUD.activeSelf);
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
    private RectTransform[] pages;
}
