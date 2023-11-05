using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string SaveFilePath;
    private SaveData SaveData;

    void Start()
    {
        SaveFilePath = Application.persistentDataPath + "/save-data.json";
        SaveData = Load() ?? GetFreshSaveData();
    }

    public void SaveLevelProgress(string levelName)
    {
        SaveData.LevelData.Find(level => level.Name == levelName).IsComplete = true;
        
        var json = JsonUtility.ToJson(SaveData);

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            using var fileStream = new FileStream(SaveFilePath, FileMode.Create);
            using var streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(json);
        }
        else
        {
            PlayerPrefs.SetString("save-data", json);
            PlayerPrefs.Save();
        }
    }

    public SaveData Load()
    {
        if (HasSaveData())
        {
            string json;

            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                using var streamReader = new StreamReader(SaveFilePath);
                json = streamReader.ReadToEnd();
            }
            else
            {
                json = PlayerPrefs.GetString("save-data");
            }

            return JsonUtility.FromJson<SaveData>(json);
        }

        return null;
    }

    public void DeleteSaveData()
    {
        if (HasSaveData())
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                File.Delete(SaveFilePath);
            }
            else
            {
                PlayerPrefs.DeleteKey("save-data");
            }

            SaveData = GetFreshSaveData();
        }
    }

    public bool HasSaveData()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            return File.Exists(SaveFilePath);
        }
        else
        {
            return PlayerPrefs.GetString("save-data") != "";
        }
    }

    private SaveData GetFreshSaveData()
    {
        return new SaveData(
            new List<LevelData>()
                {
                    new LevelData("Level1Village", false),
                    new LevelData("Level2MountainBase", false),
                    new LevelData("Level2-2", false),
                    new LevelData("Level2-3", false),
                    new LevelData("Level2-4", false),
                    new LevelData("Level2-5", false),
                    new LevelData("Level2-6", false)
                }
            );
    }
}
