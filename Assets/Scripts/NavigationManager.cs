using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    public static NavigationManager instance;
    public Room startingRoom;
    public Room currentRoom;
    public List<Room> rooms; //needed to restore room upon load
    public AudioClip transformation; //added for transformation
    public AudioClip detransformation; //added for detransformation
    AudioSource Audio;//added for sounds
    public int health; //added health for restart
    public string healthText;

    public Exit toKeyNorth; //needed to turn exit to visible from hidden

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        InputManager.instance.onRestart += ResetGame;
        health = 2; //added to set health
        //Debug.Log(startingRoom.description);
        //toKeyNorth.isHidden = true;
        //currentRoom = startingRoom;
        //Unpack();

    }

    public void ResetGame()
    {
        health = 2;//reset health
        currentRoom = startingRoom;
        Unpack();
    }

    void Unpack()
    {
        string description = currentRoom.description;
        exitRooms.Clear();
        if (currentRoom.transformed) // for playing transformation sound
        {
            Audio.clip = transformation;
            Audio.Play();
        }
        if (currentRoom.detransformed) // for playing destranformation sound
        {
            Audio.clip = detransformation;
            Audio.Play();
        }
        if (currentRoom.damage) //to cause damage
        {
            health += -1;
            if (health <= 0) //reset game at zero health
            {
                InputManager.instance.UpdateStory(description);
                InputManager.instance.userInput.text = "";// added to maybe fix an issue
                InputManager.instance.userInput.ActivateInputField();
                ResetGame();
            }
            healthText = "Health = " + health + "/2";
            InputManager.instance.UpdateStory(healthText);
        }
        if (currentRoom.heal) //Heal when needed
        {
            health = 2;
            healthText = "Health = " + health + "/2";
            InputManager.instance.UpdateStory(healthText);
        }
        foreach (Exit e in currentRoom.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitRooms.Add(e.direction.ToString(), e.room);
            }
        }

        InputManager.instance.UpdateStory(description);
    }

    public bool SwitchRooms(string direction)
    {
        if (exitRooms.ContainsKey(direction)) // if that exit exists
        {
                currentRoom = exitRooms[direction];
                Unpack();
            return true;
        }
        return false;
    }

    public void SwitchRooms(Room room)
    {
        currentRoom = room;
        Unpack();
    }

    Exit getExit(string direction)
    {
        foreach (Exit e in currentRoom.exits)
        {
            if(e.direction.ToString() == direction)
            {
                return e;
            }
        }
        return null;
    }

    public Room GetRoomFromName(string name)
    {
        foreach(Room aRoom in rooms)
        {
            if(aRoom.name == name)
            {
                return aRoom;
            }
        }

        return null;
    }
}
