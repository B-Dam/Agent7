using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public Sprite crosshair_0;
    public Sprite crosshair_1;
    
    // Start is called before the first frame update
    void Start()
    {
       Renderer spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            Renderer.sprite = crosshair_0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Renderer.sprite = crosshair_1;
        }
    }
}
