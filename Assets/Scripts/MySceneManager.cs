using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    public Button TittleButton;
    public Button IntroduceButton;

    public Image IntroduceButtonImg;

    public bool isClear;
    public bool isHiddenClear;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

         DontDestroyOnLoad(gameObject);

        isClear = PlayerPrefs.GetInt("FirstClear", 0) == 1;
        isHiddenClear = PlayerPrefs.GetInt("HiddenClear", 0) == 1;
    }
    void Start()
    {
        UpdateButtonInteractability();
    }

    void UpdateButtonInteractability()
    {
        if (TittleButton != null)
        {
            TittleButton.interactable = isClear; // 클리어 상태에 따라 활성화
        }
        if (IntroduceButton != null)
        {
            IntroduceButton.interactable = isClear; // 클리어 상태에 따라 활성화

            // 이미지 색상 초기화
            IntroduceButton.GetComponent<Image>().color =
            isClear ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    public void Clear()
    {
        isClear = true;
        PlayerPrefs.SetInt("FirstClear", 1);
        PlayerPrefs.Save();
    }

    public void HiddenClear()
    {
        isHiddenClear = true;
        PlayerPrefs.SetInt("HiddenClear", 1);
        PlayerPrefs.Save();
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("isClear value: " + isClear);

        if (scene.name == "StartScene")
        {
            TittleButton = GameObject.Find("TittleButton").GetComponent<Button>();
            IntroduceButton = GameObject.Find("IntroduceButton").GetComponent<Button>();
            IntroduceButtonImg = GameObject.Find("IntroduceButton").GetComponent <Image>();

            UpdateButtonInteractability(); // 클리어 상관없이 버튼 상태 업데이트

            if (isClear)
            {
                Debug.Log("버튼 잠금 해제");
                if (IntroduceButtonImg != null)
                    IntroduceButtonImg.color = Color.white;
            }

            if (isHiddenClear)
            {
                Debug.Log("왕관 출력");
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetClear()
    {
        isClear = false; // 클리어 상태를 false로 설정
        PlayerPrefs.SetInt("FirstClear", 0); // PlayerPrefs에서 클리어 상태 초기화
        PlayerPrefs.Save();
        UpdateButtonInteractability(); // 버튼 상태 업데이트
    }
}