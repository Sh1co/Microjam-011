using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }
    }

    
    void Update()
    {
        _animator.speed = _gc.slowMoActive ? _gc.slowedTimeScale : 1f;
    }

    private Animator _animator;
    private GlobalController _gc;
}
