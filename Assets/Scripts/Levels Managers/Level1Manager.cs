using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager instance;
    public float score;
    [SerializeField] TextMeshProUGUI ScoreTxt, winPnlScoreTxt;
    [SerializeField] Slider psycoHealth;
    float _psycoMaxHealht = 100;
    private void Awake() => instance = this;

    public GameObject WinPnl, LosePnl;

    // Start is called before the first frame update
    void Start()
    {
        psycoHealth.maxValue = _psycoMaxHealht;
        psycoHealth.value = _psycoMaxHealht;
        GameManager.instance.isWin = false;
        LosePnl.gameObject.SetActive(false);
        WinPnl.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
        ScoreTxt.text = "Score: " + score.ToString();

        DecreasePsycoHealth();
    }

    public void ChangePsycoHealth(float amount)
    {
        psycoHealth.value += amount;
    }

    public void Lost()
    {
        LosePnl.SetActive(true);
    }
    void DecreasePsycoHealth()
    {
        if (!GameManager.instance.isWin)
        {
            if (psycoHealth.value > 1)
                psycoHealth.value -= Time.deltaTime;
            else
                Lost();
        }
    }


    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.isWin = true;
        WinPnl.SetActive(true);
     


    }
  

    
}