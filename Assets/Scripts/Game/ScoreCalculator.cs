using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreCalculator : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject coinsUI;
    public GameObject coinsStats;
    public GameObject gameFinished;
    public GameObject gameFinishedTitle;
    public GameObject nextLevelBtn;
    public int coins;
    public bool isLevelComplete, isLevelFailed;
    private void Start()
    {
        coins = 0;
        isLevelComplete = false;
        isLevelFailed = false;
        gameFinished.SetActive(false);
        nextLevelBtn.SetActive(false);
        coinsUI.GetComponent<TextMeshProUGUI>().text = "Coins : " + coins;
        coinsStats.GetComponent<TextMeshProUGUI>().text = "Coins : " + coins;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) targetPlayer = player.transform;
    }

    void Update()
    {
        if (targetPlayer != null && targetPlayer.transform.position.y < -1.0f || isLevelFailed)
        {
            if (targetPlayer != null) Destroy(targetPlayer.gameObject);
            gameFinished.SetActive(true);
            gameFinishedTitle.GetComponent<TextMeshProUGUI>().text = "Game over !!!";
        }

        if (isLevelComplete)
        {
            gameFinished.SetActive(true);
            gameFinishedTitle.GetComponent<TextMeshProUGUI>().text = "Level complete !";
            nextLevelBtn.SetActive(true);

            LevelDataList levelDataList = LevelManager.Instance.LoadLevelData();
            int currentLevel = GameManager.Instance.currentLevel;

            for (int i = 0; i < levelDataList.levels.Count; i++)
            {
                if (levelDataList.levels[i].levelNum == currentLevel)
                {
                    if (i != levelDataList.levels.Count-1)
                    {
                        levelDataList.levels[i + 1].levelInfo.unlock = true;
                        LevelManager.Instance.SaveLevelData(levelDataList);
                        break;
                    }
                }
            }
        }
        coinsUI.GetComponent<TextMeshProUGUI>().text = "Coins : " + coins;
        coinsStats.GetComponent<TextMeshProUGUI>().text = "Coins : " + coins;
    }

    public void ClickRestartBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickNextLevelBtn()
    {
        if (LevelManager.Instance.totalLevels != GameManager.Instance.currentLevel + 1)
        {
            GameManager.Instance.currentLevel += 1;
        }
        else
        {
            Debug.LogWarning("You completed all levels !!");
            SceneManager.LoadScene("Menu");
        }
        SceneManager.LoadScene("Game");
    }
    public void ClickMenuBtn()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

}
