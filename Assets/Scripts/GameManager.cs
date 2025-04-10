using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject GameOverPanel;
    public GameObject FirstClearPanel;
    public GameObject ClearPanel;
    // public GameObject Crosshair; // 커서 활성 비활성용

    public AudioSource sfxSource;
    public AudioClip successSFX;
    public AudioClip failSFX;
    public AudioClip alarmSFX;

    public int CardCount = 0;
    float time = 30.0f;
    public bool isHidden = false;
    public bool isClear = false;
    // public bool isGameOver = false;  // 커서 활성 비활성용

    public bool firstClear = false;
    public bool hasShownFirstClearPanel = false;
    private bool alarmPlaying = false;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();

        firstClear = PlayerPrefs.GetInt("FirstClear", 0) == 1;
    }

    void Start()
    {
        isClear = false;
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "HiddenScene")
        {
            if (!isHidden)
            {
                isHidden = true;
                time = 15.0f;
            }
        }
    }

    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 10.0f)
        {
            animator.SetBool("isLowTime", true);

            if (!alarmPlaying)
            {
                sfxSource.clip = alarmSFX;
                sfxSource.loop = false; // 루프 활성화용
                sfxSource.volume = 0.03f;
                sfxSource.Play();
                alarmPlaying = true;
            }
        }
        else
        {
            animator.SetBool("isLowTime", false);

            if (alarmPlaying)
            {
                sfxSource.Stop();
                alarmPlaying = false;
            }
        }

        if (time < 0.0f)
        {
            GameOver();
            // isGameOver = true; // 커서 활성 비활성용
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            if (sfxSource != null && successSFX != null)
            {
                sfxSource.PlayOneShot(successSFX, 0.05f); // 성공 효과음 재생
            }

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            CardCount -= 2;

            if (CardCount == 0)
            {
                if (SceneManager.GetActiveScene().name == "HiddenScene")
                {
                    MySceneManager.instance.HiddenClear();
                }
                else
                {
                    MySceneManager.instance.Clear();
                }

                ClearLevel();

                if (hasShownFirstClearPanel == false)
                {
                    ClearPanel.SetActive(true);
                }

                Time.timeScale = 0;
            }
        }
        else
        {
            if (sfxSource != null && failSFX != null)
            {
                sfxSource.PlayOneShot(failSFX, 0.03f); // 실패 효과음 재생
            }

            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void GameOver()
    {
        // if (isGameOver) return; // 커서 활성 비활성용
        // Destroy(Crosshair); // 커서 활성 비활성용
        // Cursor.visible = true; // 커서 활성 비활성용

        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void ClearLevel()
    {
        if (!firstClear)
        {
            firstClear = true;

            // PlayerPrefs에 저장
            PlayerPrefs.SetInt("FirstClear", 1);
            PlayerPrefs.Save();

            FirstClearPanel.SetActive(true);
            hasShownFirstClearPanel = true;
        }
    }
}
