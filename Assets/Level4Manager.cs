using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Level4Manager : MonoBehaviour
{
    public static Level4Manager instance;
    public float score;
    [SerializeField] TextMeshProUGUI ScoreTxt, winPnlScoreTxt;
    [SerializeField] Slider psycoHealth;
    public float _psycoMaxHealht;
    private void Awake() => instance = this;


    public GameObject WinPnl, LosePnl, SaveHafePnl;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.isWin = false;
        LosePnl.gameObject.SetActive(false);
        WinPnl.gameObject.SetActive(false);
        psycoHealth.maxValue = _psycoMaxHealht;
        psycoHealth.value = _psycoMaxHealht;
        Time.timeScale = 1;
        SaveHafePnl.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DecreasePsycoHealth();
    }

    public void Win()
    {
        GameManager.instance.isWin = true;
        WinPnl.gameObject.SetActive(true);
        print("you win");
    }

    public void SaveBnt()
    {
        Time.timeScale = 1;
        print("Saved");
    }

    public void HafeBtn()
    {
        print("Hafed");
        Time.timeScale = 1;
        ChangePsycoHealth(_psycoMaxHealht);
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
