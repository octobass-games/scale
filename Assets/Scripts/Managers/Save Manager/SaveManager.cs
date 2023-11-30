using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string SaveFilePath;
    private SaveData SaveData;

    void Awake()
    {
        SaveFilePath = Application.persistentDataPath + "/save-data.json";
        SaveData = Load() ?? GetFreshSaveData();
    }

    public LevelData GetLevelData(string levelName)
    {
        return SaveData.LevelData.Find(level => level.Name == levelName);
    }

    public void SaveLevelProgress(string levelName, bool levelCollectableFound, bool levelClueFound)
    {
        var level = SaveData.LevelData.Find(level => level.Name == levelName);
        
        level.IsComplete = true;
        level.CollectableFound = levelCollectableFound;
        level.ClueFound = levelClueFound;
        
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
                    new LevelData("Level1Village", false, "", false, "Clue-1", false),
                    new LevelData("Level2-1", false, "pineapple", false, null, false),
                    new LevelData("Level2-2", false, "pumpkin", false, "Clue-2", false),
                    new LevelData("Level2-3", false, "carrot", false, null, false),
                    new LevelData("Level2-4", false, "watermelon", false, "Clue-3", false),
                    new LevelData("Level3-1", false, "brocolli", false, "Clue-4", false),
                    new LevelData("Level3-2", false, "strawberry", false, null, false),
                    new LevelData("Level3-3", false, "lemon", false, null, false),
                    new LevelData("Level3-4", false, "bread", false, "Clue-5", false),
                    new LevelData("Level4-1", false, "cauliflower", false, "Clue-6", false),
                    new LevelData("Level4-2", false, "blueberry", false, null, false),
                    new LevelData("Level4-3", false, "coconut", false, "Clue-7", false),
                    new LevelData("Level4-4", false, "", false, "Clue-8", false),
                    new LevelData("Level5-1", false, "icecream", false, null, false),
                    new LevelData("Level5-2", false, "lettuce", false, null, false),
                    new LevelData("Level5-3", false, "cupcake", false, "Clue-9", false),
                    new LevelData("Level5-4", false, "chocolate-cake", false, "Clue-10", false),
                }
            );
    }
}
