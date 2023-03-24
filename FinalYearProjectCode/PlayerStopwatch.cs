using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;

public class PlayerStopwatch : MonoBehaviour
{
    
    public static int stopwatch = 1;

    float runTime;
    float finalTime;

    public TextMeshProUGUI runTimeText;
    public TextMeshProUGUI finalTimeText;

    private string timesFilePath;

    void Start() {
        runTime = 0;
        stopwatch = 1;
        
        timesFilePath = Application.dataPath + "/playertimes.txt";
    }

    void Update() {
        TimeSpan time = TimeSpan.FromSeconds(runTime);
    
        if (stopwatch == 1) {
            runTime = runTime + Time.deltaTime;
            runTimeText.text = time.ToString(@"mm\:ss\:fff");
        } else if (stopwatch == 2) {
            runTime = finalTime;
            finalTimeText.text = runTimeText.text;
            writeTimesToFile();
            stopwatch = 3;
        }
        
    }

    void writeTimesToFile() {

        if (File.Exists(timesFilePath)) {
            using (var writeToFile = new StreamWriter(timesFilePath, append: true)) {
                writeToFile.WriteLine($"{Login.usernameLogged}:{finalTimeText.text}");
                print("Time Logged.");
            }
        } else {
            File.WriteAllText(timesFilePath, string.Empty);
            print("File not found.");
            writeTimesToFile();
        }
    }
}
