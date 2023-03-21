using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RegisterAccount : MonoBehaviour {
    
    public TextMeshProUGUI usernameInp;
    public TextMeshProUGUI passwordInp;
    public Button registerButton;
    public Button loginButton;
    public GameObject levelMenu;
    public GameObject loginMenu;
    ArrayList accounts;
    
    void Start() {
        registerButton.onClick.AddListener(writeToFile);

        string accountPath = Application.dataPath + "/accounts.txt";

        if (File.Exists(accountPath)) {
            accounts = new ArrayList(File.ReadAllLines(accountPath));
        } else {
            File.WriteAllText(accountPath, string.Empty);
            print("File not found.");
        }
    }

    void writeToFile() {
        bool exists = false;

        string accountPath = Application.dataPath +  "/accounts.txt";
        string[] readLines = File.ReadAllLines(accountPath);

        foreach (string line in readLines) {
            if (line.Contains(usernameInp.text)) {
                exists = true;
                break;
            }
        }

        if (exists) {
            Debug.Log($"Username '{usernameInp.text}' already exists.");
        } else {
            string newAccount = $"{usernameInp.text}:{passwordInp.text}";
            File.AppendAllText(accountPath, $"{newAccount}\n");
            Debug.Log("Account Registered");
        }
    }
}
