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
        PlayerPrefs.SetString("PowerupHealth", "0000");
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
    public static void SetSavedData() {

        PlayerData data = LoadPlayer();

        if (data == null)
        {
            ResetPlayerPrefs();
        }
        else
        {
            PlayerPrefs.SetString("habitacion", data.habitacion);
            PlayerPrefs.SetInt("entrada", data.entrada);
            PlayerPrefs.SetInt("maxJumps", data.maxJumps);
            PlayerPrefs.SetInt("canDash", data.canDash);
            PlayerPrefs.SetInt("maxhp", data.maxhp);
            PlayerPrefs.SetInt("currenthp", data.currenthp);
            PlayerPrefs.SetInt("damage", data.damage);
        }
    }
}
