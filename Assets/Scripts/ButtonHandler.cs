using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // 이 버튼의 인덱스 (0 ~ 3)
    public int buttonIndex;

    // Manager 참조 (싱글톤 UITransitionManager를 사용)
    private UITransitionManager manager;

    private void Start()
    {
        // 만약 Manager가 따로 할당되어 있지 않으면, 싱글톤 인스턴스를 사용
        manager = UITransitionManager.instance;
        // 필요하다면, 이 스크립트가 붙은 버튼의 Button 컴포넌트도 이벤트에 등록 가능
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClicked);
        }
    }

    public void OnButtonClicked()
    {
        if (manager != null)
        {
            manager.OnButtonClicked(buttonIndex);
            Debug.Log("버튼 " + buttonIndex + " 클릭됨.");
        }
    }
}
