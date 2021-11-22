using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

    public static void SavePlayer(PlayerController player) 
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.cov19";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void ResetPlayerPrefs() {

        PlayerPrefs.SetInt("maxJumps", 1);
        PlayerPrefs.SetInt("entrada", 1);
        PlayerPrefs.SetString("habitacion", "Cueva1");

        PlayerPrefs.SetInt("canDash", 0);
        PlayerPrefs.SetInt("maxhp", 100);
        PlayerPrefs.SetInt("currenthp", 100);
        PlayerPrefs.SetInt("damage", 25);
    }

    public static PlayerData LoadPlayer() 
    {

        string path = Application.persistentDataPath + "/player.cov19";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log(path);

            return data;
        }
        else 
        {
            Debug.LogError("Save File not Found in " + path);
            return null;
        }

    }
}
