using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Color lockColour = new Color(0f, 0f, 0f, 0.3f);
    private Color unlockColour = new Color(1f, 1f, 1f, 1f);

    private SpriteRenderer spriteRenderer;

    public Loader.Scene scene;
    public bool unlocked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Debug.Log("Tried to Load "+scene); // DEBUG CODE
            if (unlocked) Loader.Load(scene);
        }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (unlocked)
        {
            spriteRenderer.color = unlockColour;
        }
        else
        {
            spriteRenderer.color = lockColour;
        }
    }
}
