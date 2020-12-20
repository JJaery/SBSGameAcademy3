using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Monster _target;
    private float _time;
    private float _progress = 0;
    private Vector3 _startPos;

    public void SetTarget(Monster target, float time)
    {
        _target = target;
        _time = time;
        _progress = 0;
        _startPos = this.transform.position;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 targetPos = _target.transform.position;
        while(_progress <= 1f)
        {
            transform.position = Vector3.Lerp(_startPos, targetPos, _progress);
            _progress += Time.deltaTime / _time;

            if (_target != null)
            {
                targetPos = _target.transform.position;
            }
            yield return null;
        }

        ///미사일 도달 시
        if (_target != null)
        {
            _target.OnHit(this);
        }
        Destroy(this.gameObject);
    }
}
