using UnityEngine;
// using UnityEngine.EventSystems; // UI 체크를 하지 않으므로 주석 처리합니다.

public class GlobalClickDetector : MonoBehaviour
{
    void Update()
    {
        // UI 요소 위에서 클릭해도 효과음을 재생하도록, IsPointerOverUI() 체크를 제거합니다.
        if (Input.GetMouseButtonDown(0))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayShootSFX();
            }
        }
    }

    /*
    // 만약 UI 요소 위에서 클릭 시 효과음을 재생하지 않고 싶다면, 기존 함수 사용.
    bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    */
}
