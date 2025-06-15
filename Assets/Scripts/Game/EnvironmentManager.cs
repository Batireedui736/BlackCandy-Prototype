using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject ground;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject[] obstacles = new GameObject[3];
    public GameObject coin;
    public GameObject finishGate;
    public int levelLength;

    void Start()
    {
        LevelDataList levelDataList = LevelManager.Instance.LoadLevelData();
        int currentLevel = GameManager.Instance.currentLevel;

        for (int i = 0; i < levelDataList.levels.Count; i++)
        {
            if (levelDataList.levels[i].levelNum == currentLevel)
            {
                levelLength = levelDataList.levels[i].levelInfo.levelLenght;
                break;
            }
        }

        for (int i = 0; i < levelLength; i++)
        {
            if (i != levelLength - 1) Instantiate(ground, new Vector3(0, 0, i * 10), Quaternion.identity);
            else Instantiate(finishGate, new Vector3(0, 0, i * 10), Quaternion.identity);

            if (i >= 2 && i < levelLength - 1)
            {
                float obstacleX = Random.Range(-3.5f, 3.5f);
                Vector3 obstaclePos = new Vector3(obstacleX, 0, i * 10);
                Instantiate(obstacles[Random.Range(0, 3)], obstaclePos, Quaternion.identity);

                float coinX;
                do
                {
                    coinX = Random.Range(-3.5f, 3.5f);
                }
                while (Mathf.Abs(coinX - obstacleX) < 2.0f); 

                Vector3 coinPos = new Vector3(coinX, 0, (i * 10)+5.0f);
                Instantiate(coin, coinPos, Quaternion.identity);
            }
        }

        Instantiate(player, new Vector3(0, 3, 0), Quaternion.identity);
        Instantiate(playerCamera);
    }
}