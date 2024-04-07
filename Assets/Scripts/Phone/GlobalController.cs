using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{
    public float slowedTimeScale = .1f;
    public bool slowMoActive = false;
    public bool gamePaused = false;
    public bool startWithPhone = false;
    public int initCharge = 0;


    public void PlayerLost()
    {
        Debug.Log("Game lost");
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
