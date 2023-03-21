using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour {

    public TextMeshProUGUI usernameInp;
    public TextMeshProUGUI passwordInp;
    public Button registerButton;
    public Button loginButton;
    public GameObject levelMenu;
    public GameObject loginMenu;
    ArrayList accounts;

    // Start is called before the first frame update
    void Start() {
        loginButton.onClick.AddListener(accountAccepted);

        string accountPath = Application.dataPath + "/accounts.txt";
        
        if (File.Exists(accountPath)) {
            accounts = new ArrayList(File.ReadAllLines(accountPath));
        } else {
            Debug.Log("accounts.txt file not found");
        }
    }

    void accountAccepted() {
        bool exists = false;

        string accountPath = Application.dataPath +  "/accounts.txt";
        string[] readLines = File.ReadAllLines(accountPath);

        foreach (string line in readLines) {
            string[] items = line.Split(':');

            if (items[0].Equals(usernameInp.text) && items[1].Equals(passwordInp.text)) {
                exists = true;
                break;
            }
        }

        if (exists) {
            Debug.Log($"{usernameInp.text} has logged in successfully");
            levelMenu.SetActive(true);
            loginMenu.SetActive(false);
        } else {
            Debug.Log("Incorrect account details");
        }
    }

}
