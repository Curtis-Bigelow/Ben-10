using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings.SplashScreen;

public class SizeController : MonoBehaviour
{
    public Text displayText;
    public Text placeholderText;
    public Text playerText;

    private bool size;
    // Start is called before the first frame update
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        size = toggle.isOn;
        int pref = PlayerPrefs.GetInt("size", 1);
        if (pref == 1)
        {
            toggle.isOn = true;
            size = true;
        }
        else
        {
            toggle.isOn = false;
            size = false;
        }
        setSize();
        toggle.onValueChanged.AddListener(ProcessChange);
    }

    void ProcessChange(bool value)
    {
        size = value;
        PlayerPrefs.SetInt("size", size ? 1 : 0);
        setSize();
    }

    void setSize() //Changes the fontsize when toggled
    {
        if (size)
        {
            displayText.fontSize = 28;
            placeholderText.fontSize = 28;
            playerText.fontSize = 28;
        }
        else
        {
            displayText.fontSize = 24;
            placeholderText.fontSize = 24;
            playerText.fontSize = 24;
        }
    }

}
