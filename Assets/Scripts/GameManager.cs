using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public TMP_Text counterEgg;
    public TMP_Text hint;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject androidCon;




    //array of guards since there will be multiple guards in the game
    public GuardController[] guards;
    public PlayerMovement fox;


    public int totalEggs;
    public int collectedEggs;

    public AudioSource source;
    public AudioClip winclip;


    void Awake()
    {

        instance = this;

    }


    void Start()
    {

        StartNewLevel();

    }


    public void StartNewLevel()
    {
        collectedEggs = 0;

        totalEggs = GameObject.FindGameObjectsWithTag("Egg").Length;
        fox = GameObject.FindGameObjectWithTag("Fox").GetComponent<PlayerMovement>();
        GameObject[] guardObjects = GameObject.FindGameObjectsWithTag("Guard");
        guards = new GuardController[guardObjects.Length];

        for (int i = 0; i < guardObjects.Length; i++)
        {
            guards[i] = guardObjects[i].GetComponent<GuardController>();

        }
        newUI();

        UpdateCounter();

    }

    public void CollectEgg()
    {
        collectedEggs++;
        UpdateCounter();

    }

    void UpdateCounter()
    {
        if (counterEgg != null)
            counterEgg.text = "Eggs: " + collectedEggs + "/" + totalEggs;



        if (collectedEggs >= totalEggs)
        {
            hint.text = "All eggs collected! Return to the starting point!";
        }
    }

    public void ReachPoint()
    {
        if (collectedEggs >= totalEggs)
        {

            source = GameObject.Find("sfx").GetComponent<AudioSource>();
            winclip = Resources.Load<AudioClip>("Audio/win");
            source.clip = winclip;
            source.Play();
            WinGame();
        }
        else
        {

            hint.text = "You need to collect all the eggs then come back here!";
        }
    }

    public void LeavePoint()
    {
        hint.text = "Collect all the eggs!";
    }



    public void WinGame()
    {
        if (winScreen != null)
            winScreen.SetActive(true);


        counterEgg.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
        StopGame();

        Debug.Log("You Win!!");
    }

    public void GameOver()
    {
        if (loseScreen != null)
            loseScreen.SetActive(true);
        counterEgg.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
        StopGame();



    }

    public void StopGame()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].enabled = false;
        }
        fox.enabled = false;



    }


    public void newUI()
    {
        counterEgg = GameObject.Find("counterEgg").GetComponent<TMP_Text>();
        hint = GameObject.Find("Hint").GetComponent<TMP_Text>();
        winScreen = GameObject.Find("winScreen");
        loseScreen = GameObject.Find("loseScreen");


#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL

        if (Application.isMobilePlatform || SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (androidCon != null) androidCon.SetActive(true);
        }
        else
        {
            if (androidCon != null) androidCon.SetActive(false);
        }

#else
        if (androidCon != null) androidCon.SetActive(false);
#endif


        winScreen.SetActive(false);
        loseScreen.SetActive(false);

    }


}