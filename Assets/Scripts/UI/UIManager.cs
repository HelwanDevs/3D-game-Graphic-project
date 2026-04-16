using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text counterEgg;
    public TMP_Text hint;
    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {
        winScreen = GameObject.Find("winScreen");
        loseScreen = GameObject.Find("loseScreen");
        GameManager.instance.newUI(this);

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    void Awake()
    {
        counterEgg = GameObject.Find("counterEgg").GetComponent<TMP_Text>();
        hint = GameObject.Find("Hint").GetComponent<TMP_Text>();
        winScreen = GameObject.Find("winScreen");
        loseScreen = GameObject.Find("loseScreen");


    }



}