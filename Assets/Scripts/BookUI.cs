using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> pages;

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
        if (!Input.GetKeyDown(KeyCode.I)) return;
        
        _gc.gamePaused = true;


    }

    private PhoneController _phone;
    private GlobalController _gc;
}