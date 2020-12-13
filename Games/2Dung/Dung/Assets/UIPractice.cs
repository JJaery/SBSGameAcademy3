using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI 요소가 들어가 있는 네임스페이스
using UnityEngine.UI;

public class UIPractice : MonoBehaviour
{
    public Text _targetText;
    public Image _targetImage;
    public Toggle _targetTogle;
    public Slider _targetSlider;
    public Dropdown _targetDropDown;
    public InputField _targetInputField;

    int i = 0;

    #region 기존에 배운 것들 접어놓기
    public void OnClickButton()
    {
        _targetText.text = $"버튼이 {++i}회 눌렸습니다.";
        _targetImage.sprite = null;
        //_targetTogle.isOn = !(_targetTogle.isOn); //앞에 느낌표는 뒤에 있는 bool값을 반전
    }

    public void OnChangedToggleValue()
    {
        _targetText.text = $"해당 토글이 눌렸습니다. - 결과 : {_targetTogle.isOn}";
    }

    public void OnChangedSliderValue()
    {
        _targetText.text = $"슬라이더가 변경되었습니다. - 결과 : {_targetSlider.value} , 노멀라이즈 : {_targetSlider.normalizedValue})";
        _targetImage.rectTransform.sizeDelta = new Vector2(100 * _targetSlider.normalizedValue, 100 * _targetSlider.normalizedValue);
    }

    public void OnChangedDropDownIndex()
    {
        _targetText.text = $"드롭다운이 변경되었습니다. - 결과 : {_targetDropDown.value}";
    }
    #endregion

    public void OnEndEdit()
    {
        _targetText.text = $"입력이 끝났습니다.";
    }
    public void OnChangedInputField()
    {
        _targetText.text = _targetInputField.text;
    }

}
