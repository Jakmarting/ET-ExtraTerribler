using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserImpacts : MonoBehaviour
{
    private float impactTimer = 0.5f;
    private float impactTime = 0;
    private bool fade = false;


    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    private void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    void Update()
    {
        impactTime += Time.deltaTime;
        if (impactTime > impactTimer && !fade)
        {
            fade = true;
            StartCoroutine(FadeTo(0.0f, 1.0f));
        }
    }
}
