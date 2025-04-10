using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITransitionManager : MonoBehaviour
{
    // Singleton (옵션)
    public static UITransitionManager instance;

    // 클릭된 버튼 기억용 <<<<<<<<<<<<<<<
    private int lastClickedIndex = -1;

    [Header("버튼 관련 (4개)")]
    public Animator buttonAnimator0;    // 버튼 0에 붙은 Animator
    public Animator buttonAnimator1;    // 버튼 1에 붙은 Animator
    public Animator buttonAnimator2;    // 버튼 2에 붙은 Animator
    public Animator buttonAnimator3;    // 버튼 3에 붙은 Animator

    // (필요하면 버튼 GameObject들도 Inspector에서 할당)
    public Button button0, button1, button2, button3;
    // 각 버튼에 대응하는 텍스트 (순서대로 4개)
    public string[] buttonTexts;

    [Header("텍스트 패널 관련")]
    public GameObject textPanel;         // 텍스트 패널 (CanvasGroup 또는 별도 Animator 적용)
    public Animator textPanelAnimator;   // 텍스트 패널 Animator (ShowText, HideText 트리거 사용)
    public Text textPanelText;           // 텍스트 패널의 Text 컴포넌트

    [Header("애니메이션 딜레이")]
    public float buttonAnimDelay = 0.5f; // 버튼 애니메이션 딜레이
    public float restoreDelay = 0.5f;    // 복원 딜레이 (텍스트 창 확인 후)


    private void Awake()
    {
        // Singleton 패턴 (옵션)
        if (instance == null)
        {
            instance = this;
            // 여러 씬에서 유지하려면 아래 주석 해제 가능
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 각 버튼에서 호출할 메서드입니다. (ButtonHandler에서 인덱스 전달)
    /// 반복문 대신 인덱스별로 직접 처리합니다.
    /// </summary>
    /// <param name="index">클릭한 버튼의 인덱스 (0 ~ 3)</param>
    public void OnButtonClicked(int index)
    {
        // 어떤 버튼이 눌렸는지 저장 <<<<<<<
        lastClickedIndex = index;

        if (index == 0)
        {
            buttonAnimator0.SetTrigger("MoveToTopLeft");
            buttonAnimator1.SetTrigger("FadeOut");
            buttonAnimator2.SetTrigger("FadeOut");
            buttonAnimator3.SetTrigger("FadeOut");
            Debug.Log("버튼 0: MoveToTopLeft / 다른 버튼: FadeOut 트리거");
            StartCoroutine(ShowTextPanelRoutine(0, buttonAnimDelay));
        }
        else if (index == 1)
        {
            buttonAnimator0.SetTrigger("FadeOut");
            buttonAnimator1.SetTrigger("MoveToTopLeft");
            buttonAnimator2.SetTrigger("FadeOut");
            buttonAnimator3.SetTrigger("FadeOut");
            Debug.Log("버튼 1: MoveToTopLeft / 다른 버튼: FadeOut 트리거");
            StartCoroutine(ShowTextPanelRoutine(1, buttonAnimDelay));
        }
        else if (index == 2)
        {
            buttonAnimator0.SetTrigger("FadeOut");
            buttonAnimator1.SetTrigger("FadeOut");
            buttonAnimator2.SetTrigger("MoveToTopLeft");
            buttonAnimator3.SetTrigger("FadeOut");
            Debug.Log("버튼 2: MoveToTopLeft / 다른 버튼: FadeOut 트리거");
            StartCoroutine(ShowTextPanelRoutine(2, buttonAnimDelay));
        }
        else if (index == 3)
        {
            buttonAnimator0.SetTrigger("FadeOut");
            buttonAnimator1.SetTrigger("FadeOut");
            buttonAnimator2.SetTrigger("FadeOut");

            // 트리거 초기화 후 다시 설정
            buttonAnimator3.ResetTrigger("MoveToTopLeft");
            buttonAnimator3.ResetTrigger("ResetPosition"); // 혹시라도 꼬였을 경우 대비
            buttonAnimator3.Play("Idle", 0, 0f); // 상태를 강제로 초기화

            buttonAnimator3.SetTrigger("MoveToTopLeft");

            Debug.Log("버튼 3 (4번): MoveToTopLeft 트리거 전송");
            StartCoroutine(ShowTextPanelRoutine(3, buttonAnimDelay));
        }
    }

    /// <summary>
    /// 버튼 애니메이션 딜레이 후, 해당 버튼에 맞는 텍스트 패널을 표시합니다.
    /// </summary>
    /// <param name="index">버튼 인덱스</param>
    /// <param name="delay">딜레이 시간</param>
    IEnumerator ShowTextPanelRoutine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 각 버튼에 맞는 텍스트 설정 (반복문 없이 분기)
        if (buttonTexts != null && buttonTexts.Length >= 4 && textPanelText != null)
        {
            if (index == 0) textPanelText.text = buttonTexts[0];
            else if (index == 1) textPanelText.text = buttonTexts[1];
            else if (index == 2) textPanelText.text = buttonTexts[2];
            else if (index == 3) textPanelText.text = buttonTexts[3];
        }

        if (textPanelAnimator != null)
        {
            textPanelAnimator.SetTrigger("ShowText");
            Debug.Log("텍스트 패널: ShowText 트리거 전송");
        }
    }

    /// <summary>
    /// 텍스트 패널의 확인 버튼 클릭 시 호출됩니다.
    /// 텍스트 패널은 사라지고, 모든 버튼이 복원됩니다.
    /// </summary>
    public void OnTextPanelConfirm()
    {
        if (textPanelAnimator != null)
        {
            textPanelAnimator.SetTrigger("HideText");
            Debug.Log("텍스트 패널: HideText 트리거 전송");
        }
        StartCoroutine(RestoreButtonsRoutine(restoreDelay));
    }

    /// <summary>
    /// 텍스트 패널 확인 후 모든 버튼을 복원하는 코루틴입니다.
    /// 반복문 없이 각 버튼별로 FadeIn과 ResetPosition 트리거를 보냅니다.
    /// </summary>
    IEnumerator RestoreButtonsRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 모든 버튼에 대해 ResetPosition 전송
        for (int i = 0; i < 4; i++)
        {
            Animator anim = GetButtonAnimator(i);

            if (i == lastClickedIndex)
            {
                anim.SetTrigger("ResetPosition");
            }
            else
            {
                anim.SetTrigger("FadeIn");

                // FadeIn 끝나고 ResetPosition 주기 (예: 0.3초 후) <<<<<<<<
                StartCoroutine(DelayedTrigger(anim, "ResetPosition", 0.3f));
            }
        }

        Debug.Log("모든 버튼에 대해 ResetPosition (선택된 버튼은 FadeIn 제외)");
    }

    // 딜레이 후 트리거 전송하는 코루틴 <<<<<<<<<
    private IEnumerator DelayedTrigger(Animator anim, string triggerName, float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger(triggerName);
    }

    // 버튼 인덱스로 Animator 가져오는 헬퍼 함수 <<<<<<<
    private Animator GetButtonAnimator(int index)
    {
        switch (index)
        {
            case 0: return buttonAnimator0;
            case 1: return buttonAnimator1;
            case 2: return buttonAnimator2;
            case 3: return buttonAnimator3;
            default: return null;
        }
    }
}