using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCount : MonoBehaviour
{
    public Button myButton; // 유니티 에디터에서 버튼을 연결하세요.
    private int clickCount = 0; // 클릭 횟수를 저장할 변수
    public int targetClicks = 7; // 목표 클릭 횟수

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        clickCount++; // 클릭 횟수 증가

        if (clickCount >= targetClicks)
        {
            SceneManager.LoadScene(3);
            clickCount = 0;
        }
    }
}
