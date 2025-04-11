using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 배경음악 재생용 AudioSource
    public AudioSource bgmSource;
    // 효과음 재생용 AudioSource (Inspector에서 할당하세요)
    public AudioSource sfxSource;

    public AudioClip hiddenStageBGM;      // HiddenScene에서 사용할 BGM
    public AudioClip normalBGM;           // MainScene 및 기타 씬에서 사용할 BGM
    public AudioClip shootSFX;            // 클릭 시 재생할 사격 효과음
    public AudioClip clickSFX;           // 클릭시 재생할 클릭 효과음

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 여러 씬에서 유지
        }
        else
        {
            Destroy(gameObject);
        }

        // bgmSource 자동 할당 (없으면 해당 GameObject에 AudioSource가 있어야 함)
        if (bgmSource == null)
        {
            bgmSource = GetComponent<AudioSource>();
            if (bgmSource == null)
            {
                Debug.LogError("AudioSource 컴포넌트를 찾을 수 없습니다. AudioManager 오브젝트에 AudioSource를 추가하세요.");
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HiddenScene")
        {
            // HiddenScene에서는 hiddenStageBGM 재생
            if (hiddenStageBGM != null)
            {
                bgmSource.clip = hiddenStageBGM;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
        else if (scene.name == "MainScene")
        {
            // MainScene에서는 항상 새로운 normalBGM 재생
            if (normalBGM != null)
            {
                bgmSource.clip = normalBGM;
                bgmSource.loop = true;
                bgmSource.Play();
            }
            else
            {
                bgmSource.Stop();
            }
        }
        else
        {
            // 그 외의 씬에서는 normalBGM을 새롭게 재생
            if (normalBGM != null)
            {
                bgmSource.clip = normalBGM;
                bgmSource.loop = true;
                bgmSource.Play();
            }
            else
            {
                bgmSource.Stop();
            }
        }
    }

    /// <summary>
    /// 클릭 시 씬이 IntroductionScene이 아니라면 사격 효과음(shootSFX)를 재생합니다.
    /// 이 메서드를 버튼 클릭 이벤트 등에서 호출하세요.
    /// </summary>
    public void PlayShootSFX()
    {
        // IntroductionScene에서는 사격 효과음이 나오지 않도록 합니다.
        if (SceneManager.GetActiveScene().name != "IntroductionScene")
        {
            if (sfxSource != null && shootSFX != null)
            {
                sfxSource.PlayOneShot(shootSFX,0.5f);
                Debug.Log("사격 효과음 재생");
            }
            else
            {
                Debug.LogWarning("sfxSource 혹은 shootSFX가 할당되지 않았습니다.");
            }
        }
        else
        {
            if (sfxSource != null && clickSFX != null)
            {
                sfxSource.PlayOneShot(clickSFX, 1.5f);
                Debug.Log("클릭 효과음 재생");
            }
            else
            {
                Debug.LogWarning("sfxSource 혹은 clickSFX가 할당되지 않았습니다.");
            }
        }
    }
}
