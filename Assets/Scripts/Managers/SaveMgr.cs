using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveMgr : MonoBehaviour
{
    public static string GetSavingPath(){
        return Path.Combine(Application.persistentDataPath, "save.json");
    }

    public static void SavePlayerData(SerializablePlayerData playerData){
        string JsonData = JsonUtility.ToJson(playerData);

        string path = GetSavingPath();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);
            stream.Write(byteData, 0, byteData.Length);
            stream.Close();

            Debug.Log("SAVED (" + path + ")");
        }
    }

    public static SerializablePlayerData LoadPlayerData(){

        if(!IsFileExist()){
            SavePlayerData(new SerializablePlayerData());
        }

        string path = GetSavingPath();

        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            byte[] byteData = new byte[stream.Length];

            stream.Read(byteData, 0, byteData.Length);
            stream.Close();
            string JsonData = Encoding.UTF8.GetString(byteData);

            // Debug.Log("LOADED (" + path + ")");

            return JsonUtility.FromJson<SerializablePlayerData>(JsonData);
        }      
    }

    public static bool IsFileExist(){
        return File.Exists(Path.Combine(Application.persistentDataPath, "save.json"));
    }

}
