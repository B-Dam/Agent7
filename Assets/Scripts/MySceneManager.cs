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
        if (TittleButton != null)
        {
            TittleButton.enabled = false;
        }
        if (IntroduceButton != null)
        { 
            IntroduceButton.enabled = false;
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
        if (scene.name == "StartScene")
        {
            TittleButton = GameObject.Find("TittleButton").GetComponent<Button>();
            IntroduceButton = GameObject.Find("IntroduceButton").GetComponent<Button>();
            IntroduceButtonImg = GameObject.Find("IntroduceButton").GetComponent <Image>();

            if (isClear)
            {
                if (TittleButton != null)
                    TittleButton.enabled = true;
                if (IntroduceButton != null)
                    IntroduceButton.enabled = true;

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
}