using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroduceButton : MonoBehaviour
{
    // Animator 컴포넌트 (애니메이터 컨트롤러에 두 Trigger 파라미터가 있어야 합니다.)
    public Animator animator;

    // 두 개의 상태 전환을 나타내는 불리언 변수 (디버깅 및 참고용)
    public bool isButtonClick = false;  // 버튼 패널의 버튼 클릭 시 true 
    public bool isCheckClick = false;   // 텍스트 창의 확인 버튼 클릭 시 true

    // UI 버튼 (Inspector에서 할당하거나 코드로 찾아서 연결)
    public Button showTextButton;  // 예: 버튼 패널에 있는 버튼, 텍스트 창을 보여주기 위한 버튼
    public Button checkButton;     // 예: 텍스트 창에 있는 확인 버튼, 원래의 버튼 패널로 복귀

    private void Start()
    {
        // 버튼 이벤트 연결 (인스펙터에서 할당되어 있지 않은 경우 코드에서 연결)
        if (showTextButton != null)
            showTextButton.onClick.AddListener(OnShowTextButton);
        if (checkButton != null)
            checkButton.onClick.AddListener(OnCheckButton);
    }

    /// <summary>
    /// 버튼 패널의 버튼을 눌렀을 때 호출됩니다.
    /// </summary>
    public void OnShowTextButton()
    {
        // 상태 변수 설정 (디버깅이나 추가 로직에 사용 가능)
        isButtonClick = true;
        isCheckClick = false;

        // Animator Controller의 "ShowTextWindow" 트리거를 발동하여,
        // 버튼 패널 페이드 아웃 & 텍스트 창 페이드 인 애니메이션이 실행됩니다.
        if (animator != null)
        {
            animator.SetTrigger("ShowTextWindow");
            Debug.Log("ShowTextWindow Trigger 호출됨");
        }
        else
        {
            Debug.LogWarning("Animator가 할당되지 않았습니다.");
        }
    }

    /// <summary>
    /// 텍스트 창의 확인 버튼을 눌렀을 때 호출됩니다.
    /// </summary>
    public void OnCheckButton()
    {
        // 상태 변수 설정 (디버깅이나 추가 로직에 사용 가능)
        isCheckClick = true;
        isButtonClick = false;

        // Animator Controller의 "HideTextWindow" 트리거를 발동하여,
        // 텍스트 창 페이드 아웃 & 버튼 패널 페이드 인 애니메이션이 실행됩니다.
        if (animator != null)
        {
            animator.SetTrigger("HideTextWindow");
            Debug.Log("HideTextWindow Trigger 호출됨");
        }
        else
        {
            Debug.LogWarning("Animator가 할당되지 않았습니다.");
        }
    }
}
