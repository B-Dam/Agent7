using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject Front;
    public GameObject Back;

    public Animator Anim;

    public SpriteRenderer FrontImage;

    private Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        // Cursor.visible = false; // 커서 활성 비활성용

        idx = number;
        FrontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        if (Time.timeScale != 0)
        {
            Anim.SetBool("isOpen", true);
            Front.SetActive(true);
            Back.SetActive(false);

            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = this;
            }
            else
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        Anim.SetBool("isOpen", false);
        Front.SetActive(false);
        Back.SetActive(true);
    }
}
