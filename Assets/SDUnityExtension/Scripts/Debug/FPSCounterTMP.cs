using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounterTMP : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private TextMeshProUGUI textUgui;
    [SerializeField] private float refreshRate = 0.1f;
    private float timeCounter = 0f;

    private void Start()
    {
        if (text == null && textUgui == null) text = GetComponent<TextMeshPro>();
        if (text == null && textUgui == null) textUgui = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (timeCounter >= refreshRate)
        {
            var _text = $"{Mathf.Floor(1 / Time.deltaTime)} FPS";
            if (text != null)
                text.text = _text;
            else if (textUgui != null)
                textUgui.text = _text;

            timeCounter = 0f;
        }

        timeCounter += Time.deltaTime;
    }
}
