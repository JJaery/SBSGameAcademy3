using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // 1. "게임 시작해주세요" 텍스트를 반짝이는 효과 줄겁니다.
    //                           ㄴ 일정 주기로 투명처리 됬다가 풀리는 형태
    // 2. 키를 입력할 시 Game Scene으로 이동하는 기능 추가
    //      ㄴ 마우스 입력 시 할것인가?

    public Text _targetText;

    private void Update()
    {
        if(Input.anyKeyDown == true)
        {
            // 동기 씬 로드
            // 해당 씬이 로드 될때까지 게임이 멈춥니다.
            // 로딩 진행바 구현 X
            // LoadSceneAsync보다 로딩속도가 빠릅니다.
            //SceneManager.LoadScene("Scenes/Game");

            // 비동기 씬 로드
            // 해당 씬이 로드안되더라도 게임이 안 멈춥니다.
            // 로딩 진행바 구현 O
            // LoadScene보다 로딩속도가 느립니다.
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync("Scenes/Game");
        Debug.Log("로딩 시작");
        while (oper.isDone == false)
        {
            //Slider를 이용해서 로딩바 구현
            Debug.Log("로딩 중");
            yield return null; // 프레임 그려줌
        }
    }

    void Start()
    {
        StartCoroutine(StartFadeAnimation());
    }

    IEnumerator StartFadeAnimation()
    {
        bool isInverse = false;

        while(true)
        {
            if (isInverse == false)
            {
                _targetText.color = new Color(_targetText.color.r, _targetText.color.g, _targetText.color.b, _targetText.color.a - (1 * Time.deltaTime));
            }
            else
            {
                _targetText.color = new Color(_targetText.color.r, _targetText.color.g, _targetText.color.b, _targetText.color.a + (1 * Time.deltaTime));
            }

            if(_targetText.color.a <= 0f)
            {
                isInverse = true;
            }
            else if(_targetText.color.a >= 1f)
            {
                isInverse = false;
            } 

            yield return null; // 한프레임 재생
        }
    }
}
