using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MovementPath movementPath;
    public float speed = 5f;
    public float visionConeAngle = 90f;
    public LayerMask obstacleLayer;
    public float detectionTime = 5f; // Time to keep the enemy stopped after detecting the player

    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isPlayerDetected = false;
    private float detectionCounter = 0f;


    private void Start()
    {
        GameObject theObject = GameObject.FindGameObjectWithTag("GameController");
        if (theObject != null)
        {
            _gc = theObject.GetComponent<GlobalController>();
        }

        _rb = GetComponent<Rigidbody>();
        
        currentPointIndex = GetClosestPointIndex();

        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (!isPlayerDetected && !_gc.gamePaused)
        {
            MoveAlongPath();
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }

    void MoveAlongPath()
    {
        if (movementPath.points.Length == 0) return;

        var position = _rb.position;
        var _target = movementPath.points[currentPointIndex];
        var targetDirection = _target.transform.position - position;
        targetDirection.y = 0f; 

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation,
            Time.deltaTime * 5 * (_gc.slowMoActive ? _gc.slowedTimeScale : 1f));
        ;
    
        Vector3 dir = targetDirection.normalized;
        Vector3 velocity = dir * (speed * (_gc.slowMoActive ? _gc.slowedTimeScale : 1f));
        _rb.velocity = velocity;

        var dist = targetDirection.magnitude;
    
        if (!(dist < 1f)) return;
        currentPointIndex++;
        currentPointIndex %= movementPath.points.Length;
    }

    void OnTriggerStay(Collider other)
    {
        if (_gc.slowMoActive || _gc.gamePaused || isPlayerDetected) return;
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 directionToPlayer = other.transform.position - transform.position;
            float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenEnemyAndPlayer <= visionConeAngle / 2)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, directionToPlayer.magnitude, obstacleLayer))
                {
                    // Player detected and not obstructed by a wall
                    isPlayerDetected = true;
                    _animator.SetBool(Looking, true);
                    _player = other;
                    StartCoroutine(LookAtPlayer());
                }
            }
        }
    }

    IEnumerator LookAtPlayer()
    {
        float timeCounter = 0f;
        bool stillInVision = true;
        while (timeCounter < detectionTime)
        {
            Vector3 directionToPlayer = _player.transform.position - transform.position;
            float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenEnemyAndPlayer <= visionConeAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, directionToPlayer.magnitude, obstacleLayer))
                {
                    stillInVision = false;
                    break;
                }
            }
            else
            {
                stillInVision = false;
                break;
            }


            if (!_gc.slowMoActive) timeCounter += .1f;
            yield return new WaitForSeconds(.1f);
        }

        if (stillInVision)
        {
            _gc.PlayerLost();
        }
        
        _animator.SetBool(Looking, false);
        yield return new WaitForSeconds(1f);
        isPlayerDetected = false;
        
    }
    
    private int GetClosestPointIndex()
    {
        var closestIndex = 0;

        for (var i = 1; i < movementPath.points.Length; i++)
        {
            var position = transform.position;
            var dis1 = (movementPath.points[i].transform.position - position).magnitude;
            var dis2 = (movementPath.points[closestIndex].transform.position - position).magnitude;
            if (dis1 < dis2)
            {
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private Collider _player;
    private GlobalController _gc;
    private Rigidbody _rb;
    private Animator _animator;
    private static readonly int Looking = Animator.StringToHash("looking");
}
