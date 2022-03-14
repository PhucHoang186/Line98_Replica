using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SaveGame();
        }
    }

    public SaveData saveData;
    public void SaveGame()
    {
        string datapath = Application.persistentDataPath;// get the path to the save file 
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(datapath + "/" + saveData.savename + ".save", FileMode.Create);
        serializer.Serialize(stream, saveData);
        stream.Close();
        Debug.Log("Saved");
        
    }
    public void LoadGame()
    {
        string datapath = Application.persistentDataPath;// get the path to the save file 
        if(System.IO.File.Exists(datapath + "/" + saveData.savename + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(datapath + "/" + saveData.savename + ".save", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Loaded");
        }
    }
    public void DeleteSaveGame()
    {
        string datapath = Application.persistentDataPath;// get the path to the save file 
        if (File.Exists(datapath + "/" + saveData.savename + ".save"))
        {
            File.Delete(datapath + "/" + saveData.savename + ".save");
        }   
    }
}
[System.Serializable]
public class SaveData
{
    public string savename = "12";
    public List<Node> nodeDataList =new List<Node>();
    public GameState currentGamestateData;
}

