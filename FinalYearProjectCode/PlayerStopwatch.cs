using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerStopwatch : MonoBehaviour
{
    
    public static bool stopwatch = false;

    float currentTime;
    float finalTime;

    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI finalTimeText;

    void Start() {
        currentTime = 0;
        stopwatch = true;
    }

    void Update() {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
    
        if (stopwatch == true) {
            currentTime = currentTime + Time.deltaTime;
            currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        } else {
            currentTime = finalTime;
            finalTimeText.text = currentTimeText.text;
        }
    }
}
