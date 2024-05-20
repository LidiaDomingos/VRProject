using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject leaderboard;

    [Header("Main Menu Buttons")]
    public Button restartButton;
    public Button leaderboardButton;


    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        restartButton.onClick.AddListener(RestartGame);
        leaderboardButton.onClick.AddListener(EnableLeaderboard);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void RestartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        leaderboard.SetActive(false);
    }
    public void EnableLeaderboard()
    {
        mainMenu.SetActive(false);
        leaderboard.SetActive(true);
    }
}
