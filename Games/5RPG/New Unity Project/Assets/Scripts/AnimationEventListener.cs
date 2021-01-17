using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationEventListener : MonoBehaviour
{
    //외부에서 해당 이벤트에 구독/해제할 수 있고 해당 이벤트를 호출 시 구독된 모든 메소드들을 호출한다.
    public event Action<string> onAnimationEvent; // void 메소드 + string 파라미터가 들어가는 메소드 이벤트

    public void MeleeAttackStart()
    {
        if(onAnimationEvent != null) // null이면 아무도 구독하지 않는 상태입니다.
        {
            onAnimationEvent("MeleeAttackStart");
        }
    }

    public void MeleeAttackEnd()
    {
        if (onAnimationEvent != null) // null이면 아무도 구독하지 않는 상태입니다.
        {
            onAnimationEvent("MeleeAttackEnd");
        }
    }
}
