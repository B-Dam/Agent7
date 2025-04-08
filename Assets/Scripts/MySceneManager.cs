using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    public Button TittleButton;
    public Button IntroduceButton;

    public bool isClear;


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
    }
    void Start()
    {
        isClear = false;

        TittleButton.GetComponent<Button>().enabled = false;
        IntroduceButton.GetComponent<Button>().enabled = false;

    }

    public void Clear()
    {
        isClear = true;
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
            
            if (isClear)
            {
                TittleButton.GetComponent<Button>().enabled = true;
                IntroduceButton.GetComponent<Button>().enabled = true;
                Debug.Log("버튼 잠금 해제");
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}