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
    float scoreNum = 1000;
    int score;

    //public TextMeshProUGUI runTimeText;
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lowestScoreText;

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
            //runTimeText.text = time.ToString(@"mm\:ss\:fff");
        } else if (stopwatch == 2) {
            runTime = finalTime;
            finalTimeText.text = scoreText.text;
            writeTimesToFile();
            stopwatch = 3;
        }

        score = Mathf.RoundToInt(runTime * scoreNum);
        scoreText.text = score.ToString();
        
    }

    void writeTimesToFile() {
        
        if (File.Exists(timesFilePath)) {
            using (var writeToFile = new StreamWriter(timesFilePath, append: true)) {
                writeToFile.WriteLine($"{Login.usernameLogged}-{finalTimeText.text}");
                print("Time Logged.");
            }
            findLowestScore();
        } else {
            File.WriteAllText(timesFilePath, string.Empty);
            print("File not found.");
            writeTimesToFile();
        }
    }

    void findLowestScore() {
        List<int> scoreNumList = new List<int>();
        timesFilePath = Application.dataPath +  "/playertimes.txt";
        string[] readLines = File.ReadAllLines(timesFilePath);

        foreach (string line in readLines) {
            string[] items = line.Split('-');
            scoreNumList.Add(int.Parse(items[1]));
        }

        scoreNumList.Sort();
        int lowestScore = scoreNumList[0];
        lowestScoreText.text = "Best Score: " + lowestScore.ToString();

    }
}
