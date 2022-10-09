using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    string file, uname, ammo, health, data;

    public TextMeshProUGUI userNameText, ammoText, healthText;

    private void Awake()
    {
        Cursor.visible = true;
    }
    private void Start()
    {
        file = PlayerPrefs.GetString("FilePath");
        uname = PlayerPrefs.GetString("UserName");
        ammo = PlayerPrefs.GetInt("Ammo").ToString();
        health = PlayerPrefs.GetFloat("Health").ToString();

        data = uname + ", " + ammo + ", " + health;
        Display();
        SaveFile(file, data);
    }

    private void SaveFile(string file, string data)
    {
        File.AppendAllText(file, System.Environment.NewLine + data);
    }

    private void Display()
    {
        userNameText.text = uname;
        ammoText.text = ammo;
        healthText.text = health;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
