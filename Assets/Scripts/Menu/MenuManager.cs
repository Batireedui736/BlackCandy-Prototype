using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject chooseLevel;
    public GameObject playBtn;
    public GameObject quitBtn;

    void Start()
    {
        HideAll();
        menu.SetActive(true);
    }
    public void HideAll()
    {
        menu.SetActive(false);
        chooseLevel.SetActive(false);
    }
    public void ClickPlayBtn()
    {
        HideAll();
        chooseLevel.SetActive(true);
    }

    public void ClickQuitBtn()
    {
        Application.Quit();
    }
    public void ClickBackBtn()
    {
        HideAll();
        menu.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
