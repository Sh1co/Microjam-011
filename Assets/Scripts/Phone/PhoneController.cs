using System;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public static bool phoneActive = false;
    public int maxBattery = 4;
    public static int currentBattery = 0;
    public float slowMoDuration = 15f;
    public float currentSlowMoProgress = 0f;
    [SerializeField] private string batteryTag = "Battery";
    [SerializeField] private string phoneTag = "Phone";
    [SerializeField] private GameObject phone;
    [SerializeField] private GameObject holder;

    private void Start()
    {
        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }
        
        phoneActive = _gc.startWithPhone;
        currentBattery = _gc.initCharge;
        
        _camera = transform.parent.GetComponent<Camera>();

        if (phoneActive)
        {
            phone.SetActive(true);
            holder.SetActive(true);
        }
        else
        {
            phone.SetActive(false);
            holder.SetActive(false);
        }
    }

    void Update()
    {
        if (_gc.gamePaused) return;
        if(phoneActive)
        {
            CheckMouseClick();
            UpdateSlowMoTime();
            CheckBatteryRecharge();
        }
        else
        {
            CheckForPhone();
        }
        
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_gc.slowMoActive) return;
            if (currentBattery > 0)
            {
                currentBattery--;
                _gc.slowMoActive = true;
            }
        }

        if (!Input.GetMouseButtonDown(1)) return;
        _gc.slowMoActive = false;
        currentSlowMoProgress = 0;

    }

    private void UpdateSlowMoTime()
    {
        if (!_gc.slowMoActive) return;
        currentSlowMoProgress += Time.deltaTime;

        if (!(currentSlowMoProgress > slowMoDuration)) return;
        _gc.slowMoActive = false;
        currentSlowMoProgress = 0;

    }

    private void CheckBatteryRecharge()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        RaycastHit hit;

        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        var ray = _camera.ScreenPointToRay(screenCenter);
        
        if(!Physics.Raycast(ray, out hit,Mathf.Infinity))return;

        if (!hit.collider.CompareTag(batteryTag)) return;

        if (currentBattery == maxBattery) return;
        
        Destroy(hit.collider.gameObject);
        currentBattery = maxBattery;
    }
    
    private void CheckForPhone()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        RaycastHit hit;

        var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        var ray = _camera.ScreenPointToRay(screenCenter);
        
        if(!Physics.Raycast(ray, out hit,Mathf.Infinity))return;

        if (!hit.collider.CompareTag(phoneTag)) return;

        Destroy(hit.collider.gameObject);
        phoneActive = true;
        phone.SetActive(true);
        holder.SetActive(true);
    }
    
    private GlobalController _gc;
    private Camera _camera;
}
