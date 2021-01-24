using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageText : MonoBehaviour
{
    public Text _damageText;
    private Transform _targetTransform;
    private Vector3 _offset;

    //Screen 좌표값인지 월드좌표값인지 결정
    public void SetUI(Transform targetTransform,float damage)
    {
        _targetTransform = targetTransform;
        _damageText.text = Mathf.RoundToInt(damage).ToString();
        StartCoroutine(Animation());
    }

    /// <summary>
    /// 애니메이션효과
    /// 1. 점점 투명하게 사라짐(fade out) -> color 알파값을 천천히 줄임
    /// 2. 위로 상승함 -> transform.position을 천천히 올림
    /// </summary>
    /// <returns></returns>
    private IEnumerator Animation()
    {
        while(_damageText.color.a > 0)
        {
            _damageText.transform.position = Camera.main.WorldToScreenPoint(_targetTransform.position);
            _damageText.color =
                new Color(_damageText.color.r, _damageText.color.g, _damageText.color.b, _damageText.color.a - (0.5f * Time.deltaTime));
            _offset += Vector3.up * 20f * Time.deltaTime;
            _damageText.transform.position += _offset;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}