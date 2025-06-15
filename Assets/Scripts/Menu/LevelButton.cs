using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public GameObject padlock;
    public int levelNumber;
    LevelDataList levelDataList;

    private void Awake()
    {
        levelDataList = LevelManager.Instance.LoadLevelData();
    }

    private void Start()
    {
        string[] parts = gameObject.name.Split(' ');
        if (parts.Length == 2 && int.TryParse(parts[1], out int parsedNumber))
        {
            levelNumber = parsedNumber;
        }
        gameObject.name = "LevelBtn " + levelNumber;
        if (levelDataList.levels[levelNumber - 1].levelInfo.unlock)
        {
            padlock.SetActive(false);
        }
    }
    public void OnClick()
    {
        if (levelDataList.levels[levelNumber-1].levelInfo.unlock)
        {
            GameManager.Instance.GoToLevel(levelNumber);
        }
        else
        {
            Debug.LogWarning("Level is not unlocked");
        }
    }
}
