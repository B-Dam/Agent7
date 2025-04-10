using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject hiddenCard;

    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3,
                      4, 4, 5, 5, 6, 6, 7, 7 }; 
        arr = arr.OrderBy(x=> Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                GameObject go = Instantiate(card, this.transform);

                float x = (i % 4) * 1.2f - 1.8f;
                float y = (i / 4) * 1.73333f - 3.2f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }
            else if (SceneManager.GetActiveScene().name == "HiddenScene")
            {
                GameObject go = Instantiate(hiddenCard, this.transform);

                float x = (i % 4) * 1.2f - 1.8f;
                float y = (i / 4) * 1.73333f - 3.2f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }
        }

        GameManager.Instance.CardCount = arr.Length;
    }
}
