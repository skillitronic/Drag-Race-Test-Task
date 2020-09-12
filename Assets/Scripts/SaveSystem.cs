using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void Save(string saveFile, object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = GetPath(saveFile);

        FileStream file = File.Create(path);

        formatter.Serialize(file, saveData);

        file.Close();
    }

    public static object Load(string loadFile)
    {
        string path = GetPath(loadFile);

        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat($"Failed to load file at {path}");
            file.Close();
            return null;
        }

    }

    public static string GetPath(string saveName)
    {
        return (Application.persistentDataPath + $"/saves/{saveName}");
    }

}


