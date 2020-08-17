using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{

    public void Save(){
        Debug.Log("Saving...");

        //create a file or open a file to save to
        FileStream file = new FileStream(Application.persistentDataPath + "/saved.dat", FileMode.OpenOrCreate);

        try{
            //create Binary Formater
            BinaryFormatter formatter = new BinaryFormatter();
            //serialize method to write to the file
            formatter.Serialize(file, GameManager.Instance.gameData);
        }catch(SerializationException e){
            //log error message if there was an issue serialising the data
            Debug.LogError("There was an issue Serialising this data: " + e.Message);
        }finally{
            //close the file
            file.Close();
        }

        Debug.Log("Saved!");
    }

    public void Load(){
        Debug.Log("Loading...");

        //open a file to load from
        FileStream file = new FileStream(Application.persistentDataPath + "/saved.dat", FileMode.Open);

        try{
            //create Binary Formater
            BinaryFormatter formatter = new BinaryFormatter();
            //deserialize method to load file
            GameManager.Instance.gameData = (GameData) formatter.Deserialize(file);
        }catch(SerializationException e){
            //log error message if there was an issue serialising the data
            Debug.LogError("There was an issue Deserialising this data: " + e.Message);
        }finally{
            //close the file
            file.Close();
        }
        
        Debug.Log("Loaded!");
    }
}
