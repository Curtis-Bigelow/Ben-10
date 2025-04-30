using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.onRestart += ResetGame; // ResetGame() will be code to respond to event
        Load();
    }

    void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream afile = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState playerData =(SaveState) bf.Deserialize(afile);
            afile.Close();

           Room room = NavigationManager.instance.GetRoomFromName(playerData.currentRoom);
            if(room != null)
            {
                NavigationManager.instance.SwitchRooms(room);
            }
            NavigationManager.instance.health = playerData.health;
        }
        else
        {
            NavigationManager.instance.ResetGame();
        }
    }

    void ResetGame()
    {
        NavigationManager.instance.health = 2;
    }

    public void Save()
    {
        //set up data to save
        SaveState playerState = new SaveState();
        playerState.currentRoom = NavigationManager.instance.currentRoom.name;
        playerState.health = NavigationManager.instance.health;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream afile = File.Create(Application.persistentDataPath + "/player.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(afile, playerState);
        afile.Close();
    }

}
