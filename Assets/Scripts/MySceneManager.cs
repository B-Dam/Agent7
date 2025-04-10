using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    public Button tittleButton;
    public Button introduceButton;

    public Image introduceButtonImg;

    public GameObject rewardDeco;

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
        if (tittleButton != null)
        {
            tittleButton.interactable = isClear; // 클리어 상태에 따라 활성화
        }
        if (introduceButton != null)
        {
            introduceButton.interactable = isClear; // 클리어 상태에 따라 활성화

            // 이미지 색상 초기화
            introduceButton.GetComponent<Image>().color =
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
            tittleButton = GameObject.Find("TittleButton").GetComponent<Button>();
            introduceButton = GameObject.Find("IntroduceButton").GetComponent<Button>();
            introduceButtonImg = GameObject.Find("IntroduceButton").GetComponent <Image>();
            rewardDeco = GameObject.Find("RewardDeco");

            UpdateButtonInteractability(); // 클리어 상관없이 버튼 상태 업데이트

            if (isClear)
            {
                Debug.Log("버튼 잠금 해제");
                if (introduceButtonImg != null)
                    introduceButtonImg.color = Color.white;
            }

            if(!isHiddenClear)
            {
                HideRewardDeco();
            }

            if (isHiddenClear)
            { 
                Debug.Log("히든 클리어 조건 확인");
                ShowRewardDeco();            
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
        isHiddenClear = false;
        PlayerPrefs.SetInt("FirstClear", 0); // PlayerPrefs에서 클리어 상태 초기화
        PlayerPrefs.SetInt("HiddenClear", 0); 
        PlayerPrefs.Save();
        UpdateButtonInteractability(); // 버튼 상태 업데이트
    }

    public void HideRewardDeco()
    {
        if (rewardDeco != null)
        {
            rewardDeco.SetActive(false);
            Debug.Log("RewardDeco 숨김");
        }
    }

    // 오브젝트 다시 활성화
    public void ShowRewardDeco()
    {
        if (rewardDeco != null)
        {
            rewardDeco.SetActive(true);
            Debug.Log("RewardDeco 활성화");
        }
    }
}