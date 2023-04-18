using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverFade : MonoBehaviour
{
    // Should we be fading in? (becoming more black)
    bool shouldFadeIn;
    // How long has this been faded for
    float TimeFaded;

    // Reference to renderer
    CanvasRenderer rd;

    // Start is called before the first frame update
    void Start()
    {
        TimeFaded = 0;
        rd = GetComponent<CanvasRenderer>();
        shouldFadeIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = rd.GetColor();

        TimeFaded += Time.deltaTime;

        if (shouldFadeIn)
        {
            color.a = Mathf.Clamp(TimeFaded, 0.0f, 1.0f) / 1.0f;
        }
        else
        {
            color.a = color.a - Mathf.Clamp(TimeFaded, 0.0f, 1.0f) / 1.0f;
        }

        rd.SetColor(color);
    }

    // Fade this cover in (make it black)
    public void FadeIn()
    {
        TimeFaded = 0.0f;
        shouldFadeIn = true;
    }

    // Fade this cover out (make it transparent)
    public void FadeOut()
    {
        TimeFaded = 0.0f;
        shouldFadeIn = false;
    }
}
