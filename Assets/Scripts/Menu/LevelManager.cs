using UnityEngine;
using System.IO;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public MenuManager menuManager;
    public GameObject levelBtn;
    public LevelDataList jsonLevelData = new();
    public int totalLevels;
    private string jsonFilePath;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "leveldata.json");
        //C:\Users\Ireedui\AppData\LocalLow\DefaultCompany\BlackCandy Prototype\leveldata.json
        totalLevels = 3;

        if (!File.Exists(jsonFilePath))
        {
            for (int i = 1; i <= totalLevels; i++)
            {
                LevelData data = new();
                data.levelNum = i;
                data.levelInfo = new();

                if (i == 1) data.levelInfo.unlock = true;
                else data.levelInfo.unlock = false;

                data.levelInfo.levelLenght = (i * 10) + 10;

                jsonLevelData.levels.Add(data);
            }

            SaveLevelData(jsonLevelData);
        }
    }


    public void SaveLevelData(LevelDataList data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(jsonFilePath, json);
    }

    public LevelDataList LoadLevelData()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            LevelDataList data = JsonUtility.FromJson<LevelDataList>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Level data empty");
            return null;
        }
    }

    public void GenerateLevels ()
    {
        foreach (Transform child in menuManager.chooseLevel.transform)
        {
            if (child.gameObject.name != "BackBtn") Destroy(child.gameObject);
        }
        LevelData data = new();
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject newLevel = Instantiate(levelBtn, menuManager.chooseLevel.transform);
            newLevel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + i;
            newLevel.name = "LevelBtn " + i;
            RectTransform newLevelRectTransform = newLevel.GetComponent<RectTransform>();
            newLevelRectTransform.anchoredPosition = new Vector2(0, 0+(i*-150));
        }
    }

}

[System.Serializable]
public class LevelDataList
{
    public List<LevelData> levels = new();
}

[System.Serializable]
public class LevelData
{
    public int levelNum;
    public LevelInfo levelInfo;
}

[System.Serializable]
public class LevelInfo
{
    public bool unlock;
    public int levelLenght;
}
