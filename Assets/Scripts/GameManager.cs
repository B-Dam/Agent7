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
    public GameObject EndTxt;

    public int CardCount = 0;
    float time = 30.0f;
    public bool isHidden = false;
    public bool isClear = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2"); 

        if(time < 0)
        {
            GameOver();
        }
    }


    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            CardCount -= 2;
            if (CardCount == 0)
            {
                MySceneManager.instance.Clear();
                Time.timeScale = 0;
                EndTxt.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        EndTxt.SetActive(true);
    }
}
