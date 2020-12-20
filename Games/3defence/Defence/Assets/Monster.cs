using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform _curWayPoint;
    private Transform _targetWayPoint;
    private float _progress;
    private int _waypointIndex = 0;
    private float _moveSpeed = 1.5f;

    private void Awake()
    {
        _curWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex];
        _targetWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex + 1];
    }

    private void Update()
    {
        this.transform.position = Vector3.Lerp(_curWayPoint.position, _targetWayPoint.position, _progress);
        _progress += (Time.deltaTime * 0.12f) * _moveSpeed;
        if(_progress >= 1f)
        {
            ChangeWayPoints();
            _progress = 0;
        }
    }

    private void ChangeWayPoints()
    {
        _waypointIndex += 1;
        if (_waypointIndex >= SpawnManager.Instance.wayPoints.Length)
        {
            _waypointIndex = 0;
            _curWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex];
            _targetWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex + 1];
        }
        else
        {
            _curWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex];
            if (_waypointIndex + 1 >= SpawnManager.Instance.wayPoints.Length)
            {
                _targetWayPoint = SpawnManager.Instance.wayPoints[0];
            }
            else
            {
                _targetWayPoint = SpawnManager.Instance.wayPoints[_waypointIndex + 1];
            }
        }
    }

}
