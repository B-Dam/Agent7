using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public Sprite crosshair;

    
    // Start is called before the first frame update
    void Start()
    {
       Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        Renderer.sprite = crosshair;

        if (Input.GetMouseButtonDown(0))
        {
            transform.localScale = new Vector2(0.13f, 0.13f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector2(0.1f, 0.1f);
        }
    }
}
