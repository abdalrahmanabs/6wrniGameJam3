using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    public static Level3Manager instance;
    public float score;
    [SerializeField] TextMeshProUGUI ScoreTxt, winPnlScoreTxt;
    [SerializeField] Slider psycoHealth;
    public float _psycoMaxHealht ;
    private void Awake() => instance = this;

    public GameObject WinPnl, LosePnl;
    // Start is called before the first frame update
    void Start()
    {

        GameManager.instance.isWin = false;
        psycoHealth.maxValue = _psycoMaxHealht;
        psycoHealth.value = _psycoMaxHealht;
        LosePnl.gameObject.SetActive(false);
        WinPnl.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        ScoreTxt.text = "Score: " + score.ToString();
        DecreasePsycoHealth();
    }
    public void Win()
    {
        GameManager.instance.isWin=true;
        WinPnl.gameObject.SetActive(true) ;
        print("you win");
    }

    public void ChangePsycoHealth(float Amount)
    {
        psycoHealth.value += Amount;
        if (psycoHealth.value <= 0.1f)
            Lost();
    }
    public void Lost()
    {
      
        LosePnl.SetActive(true);
        GameManager.instance.isWin = true;//acutaly its not but i enabled it just for stop player movement
    }

    void DecreasePsycoHealth()
    {
        if (!GameManager.instance.isWin)
        {
            if (psycoHealth.value > 1)
                psycoHealth.value -= Time.unscaledDeltaTime;
            else
                Lost();
        }
    }

}
