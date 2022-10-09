using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField uNameIF;

    string uName;
    string dataFilePath;

    TextMeshProUGUI placeholderText;

    private void Start()
    {
        placeholderText = (TextMeshProUGUI)uNameIF.placeholder;
    }
    public void LogIn()
    {
        
        uName = uNameIF.text;
        if(uName == "")
        {
            placeholderText.text = "Please Enter Your Username";
        }
        else
        {
            dataFilePath = Application.dataPath + "/" + uName + ".csv";
            PlayerPrefs.SetString("FilePath", dataFilePath);
            PlayerPrefs.SetString("UserName", uName);
            CreateFile(dataFilePath);
        }
    }

    private void CreateFile(string path)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "UserName, Ammo, Health");
            SceneManager.LoadScene(1);
        }
        else
        {
            placeholderText.text = "Entered Username Already Exists";
            uNameIF.text = null;
        }
    }
}
