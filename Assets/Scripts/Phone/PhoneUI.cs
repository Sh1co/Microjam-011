using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usesHUD;
    [SerializeField] private Slider timeHUD;

    private void Start()
    {
        GameObject theObject = GameObject.FindGameObjectWithTag("Phone");
        if (theObject != null)
        {
            _phone = theObject.GetComponent<PhoneController>();
        }
        theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }
    }

    private void Update()
    {
        usesHUD.text = "Phone Uses Left: " + _phone.currentBattery;
        if (_gc.slowMoActive)
        {
            timeHUD.gameObject.SetActive(true);
            timeHUD.maxValue = _phone.slowMoDuration;
            timeHUD.value = _phone.currentSlowMoProgress;
        }
        else
        {
            timeHUD.gameObject.SetActive(false);
        }
    }

    private PhoneController _phone;
    private GlobalController _gc;
}