using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Text storyText; // the story 
    public InputField userInput; // the input field object
    public Text inputText; // part of the input field where user enters response
    public Text placeHolderText; // part of the input field for initial placeholder text
    //public Button abutton;
    //first step to creating and using delegates
    public delegate void Restart(); //Create delegate
    public event Restart onRestart;
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>(); //valid user commands

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        commands.Add("do"); //changed to do for purpose of sounding right
        commands.Add("restart"); //Added with delegate
        commands.Add("save");
        commands.Add("commands");// shows commands

        userInput.onEndEdit.AddListener(GetInput);
        //abutton.onClick.AddListener(DoSomething);
        story = storyText.text;
    }
    public void UpdateStory(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
    }

    void GetInput(string msg)
    {
        if (msg != "")
        {
            char[] splitInfo = { ' ' };
            string[] parts = msg.ToLower().Split(splitInfo); //['go', 'north']

            if (commands.Contains(parts[0])) //if valid command
            {
                if (parts[0] == "do") //wants to switch rooms
                {
                    if (NavigationManager.instance.SwitchRooms(parts[1])) //returns true or false
                    {
                        //fill in later 
                    }
                    else
                    {
                        //took out is locked part
                        UpdateStory("Exit does not exist. Try again.");
                    }
                }
                else if (parts[0] == "restart")
                {
                    if(onRestart != null)// if anyone is listening
                    {
                        onRestart();
                    }
                }
                else if (parts[0] == "save")
                {
                    GameManager.instance.Save();
                }
                else if (parts[0] == "commands")
                {
                    foreach(string s in commands) //gets each command and prints them on screen
                    {
                        UpdateStory(s);
                    }
                }
            }

        }

        // reset for next input
        userInput.text = ""; //after input from user
        userInput.ActivateInputField();
    }

}
