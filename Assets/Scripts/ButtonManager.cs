using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // 상단, 하단 패널의 RectTransform 참조 (인스펙터에서 할당)
    public RectTransform topPanel;
    public RectTransform bottomPanel;
    // 가운데 텍스트를 포함한 패널 및 텍스트 (설명용)
    public GameObject descriptionPanel;
    public Text descriptionText;

    // 애니메이션 시간과 이동할 거리 (gapSize는 두 패널 사이에 만들어질 공간 크기)
    public float animationDuration = 0.5f;
    public float gapSize = 100f;

    // 원래 패널 위치 저장(애니메이션 이후 복원할 때 사용)
    private Vector2 topPanelOriginalPos;
    private Vector2 bottomPanelOriginalPos;
    // 가운데 패널의 원래 스케일 저장
    private Vector3 descriptionOriginalScale;

    private void Start()
    {
        // 원래 위치 저장
        topPanelOriginalPos = topPanel.anchoredPosition;
        bottomPanelOriginalPos = bottomPanel.anchoredPosition;

        // descriptionPanel은 초기엔 숨김 처리하고, 원래 스케일 저장
        if (descriptionPanel != null)
        {
            descriptionOriginalScale = descriptionPanel.GetComponent<RectTransform>().localScale;
            descriptionPanel.SetActive(false);
        }
    }

    // 각 버튼의 OnClick 이벤트에 연결할 함수.
    // 버튼마다 다른 설명을 text 매개변수로 전달할 수 있음.
    public void OnButtonClicked(string text)
    {
        StopAllCoroutines();
        StartCoroutine(AnimatePanelsAndShowText(text));
    }

    IEnumerator AnimatePanelsAndShowText(string text)
    {
        // descriptionPanel 활성화 및 텍스트 설정
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
            descriptionText.text = text;
            // 가운데 패널의 RectTransform을 가져와 애니메이션 전 스케일을 0으로 설정
            RectTransform descRect = descriptionPanel.GetComponent<RectTransform>();
            descRect.localScale = Vector3.zero;
        }

        // 상단 패널은 위쪽으로, 하단 패널은 아래쪽으로 이동하도록 목표 위치 계산
        Vector2 topTargetPos = topPanelOriginalPos + new Vector2(0, gapSize / 2);
        Vector2 bottomTargetPos = bottomPanelOriginalPos - new Vector2(0, gapSize / 2);

        float elapsed = 0f;
        // 두 패널과 가운데 패널의 확장 애니메이션을 동시에 진행
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            // 패널들의 위치 애니메이션 (위/아래 이동)
            topPanel.anchoredPosition = Vector2.Lerp(topPanelOriginalPos, topTargetPos, t);
            bottomPanel.anchoredPosition = Vector2.Lerp(bottomPanelOriginalPos, bottomTargetPos, t);
            // 가운데 패널의 스케일 애니메이션 (확장)
            if (descriptionPanel != null)
            {
                RectTransform descRect = descriptionPanel.GetComponent<RectTransform>();
                descRect.localScale = Vector3.Lerp(Vector3.zero, descriptionOriginalScale, t);
            }
            yield return null;
        }
        // 최종 위치와 스케일 보정
        topPanel.anchoredPosition = topTargetPos;
        bottomPanel.anchoredPosition = bottomTargetPos;
        if (descriptionPanel != null)
        {
            RectTransform descRect = descriptionPanel.GetComponent<RectTransform>();
            descRect.localScale = descriptionOriginalScale;
        }

        // 일정 시간 후 패널들을 원래 상태로 복귀시키는 코루틴 실행 (예: 3초 후)
        yield return new WaitForSeconds(3f);
        StartCoroutine(ResetPanels());
    }

    IEnumerator ResetPanels()
    {
        float elapsed = 0f;
        Vector2 topCurrentPos = topPanel.anchoredPosition;
        Vector2 bottomCurrentPos = bottomPanel.anchoredPosition;
        // 가운데 패널은 확장된 상태(원래 스케일)를 가지고 있다고 가정
        RectTransform descRect = descriptionPanel.GetComponent<RectTransform>();

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            topPanel.anchoredPosition = Vector2.Lerp(topCurrentPos, topPanelOriginalPos, t);
            bottomPanel.anchoredPosition = Vector2.Lerp(bottomCurrentPos, bottomPanelOriginalPos, t);
            // 가운데 패널은 축소 애니메이션 (원래 스케일에서 0으로)
            descRect.localScale = Vector3.Lerp(descriptionOriginalScale, Vector3.zero, t);
            yield return null;
        }
        topPanel.anchoredPosition = topPanelOriginalPos;
        bottomPanel.anchoredPosition = bottomPanelOriginalPos;
        descRect.localScale = Vector3.zero;
        // 가운데 패널 숨김 처리
        if (descriptionPanel != null)
            descriptionPanel.SetActive(false);
    }
}
