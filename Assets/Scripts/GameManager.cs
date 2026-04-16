using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public UIManager ui;



    public int totalEggs;
    public int collectedEggs;




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        totalEggs = GameObject.FindGameObjectsWithTag("Egg").Length;

        collectedEggs = 0;
        UpdateCounter();
        if (ui.winScreen != null)
            ui.winScreen.SetActive(false);
        if (ui.loseScreen != null)
            ui.loseScreen.SetActive(false);

    }

    public void CollectEgg()
    {
        collectedEggs++;
        UpdateCounter();

    }

    void UpdateCounter()
    {
        if (ui.counterEgg != null && ui != null)
            ui.counterEgg.text = "Eggs: " + collectedEggs + "/" + totalEggs;
    }

    public void ReachPoint()
    {
        if (collectedEggs >= totalEggs)
        {
            WinGame();
        }
        else
        {

            ui.hint.text = "You need to collect all the eggs then come back here!";
        }
    }

    public void LeavePoint()
    {
        ui.hint.text = "Collect all the eggs!";
    }



    public void WinGame()
    {
        if (ui.winScreen != null)
            ui.winScreen.SetActive(true);

        Debug.Log("You Win!!");
    }

    public void GameOver()
    {
        if (ui.loseScreen != null)
            ui.loseScreen.SetActive(true);




    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Returning to main menu.");
            SceneManager.LoadScene(0);
        }
    }


    public void newUI(UIManager newUI)
    {
        ui = newUI;


        UpdateCounter();
    }


}