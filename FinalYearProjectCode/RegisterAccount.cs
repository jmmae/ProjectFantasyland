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
    
    // Start is called before the first frame update
    void Start() {
        registerButton.onClick.AddListener(writeToFile);
        loginButton.onClick.AddListener(login); //Go to level menu

        string accountPath = Path.Combine(Application.dataPath, "/accounts.txt");

        if (File.Exists(accountPath)) {
            accounts = new ArrayList(File.ReadAllLines(accountPath));
        } else {
            File.WriteAllText(accountPath, string.Empty);
        }
    }

    void login() {
        levelMenu.SetActive(true);
        loginMenu.SetActive(false);
    }

    void writeToFile() {
        bool exists = false;

        string accountPath = Application.dataPath +  "/accounts.txt";
        string[] readLines = File.ReadAllLines(accountPath);
        print(accountPath);

        //accounts = new ArrayList(File.ReadAllLines(Application.dataPath + "/accounts.txt"));
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
