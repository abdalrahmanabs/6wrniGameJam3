using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level2Manager : MonoBehaviour
{
    public static Level2Manager instance;
    public float ParckourTimer,score;
    public TextMeshProUGUI parkourTxt;
    public GameObject LosePnl, WinPnl, KillDillPnl, killMessegePnl, DillMessegePnl;
  
    public GameObject Portal,Boss;
    public  Slider psycoHealth;
    [SerializeField] float _psycoMaxHealht ;
    bool Killable = true; public bool finishParckour = false;

    public Cinemachine.CinemachineVirtualCamera mainCam;

    [SerializeField] Transform FightPosition;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Boss.gameObject.SetActive(false);
        Portal.gameObject.SetActive(false);
        psycoHealth.gameObject.SetActive(false);
        LosePnl.gameObject.SetActive(false);
        WinPnl.gameObject.SetActive(false);
        GameManager.instance.isWin = false;
        KillDillPnl.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishParckour)
            decreaseParckourtimer();
        else
        {
            parkourTxt.enabled = false;
        }

       
    }

    void decreaseParckourtimer()
    {

        
            parkourTxt.text = ParckourTimer.ToString("0");
            if (ParckourTimer > 0.1f)
                ParckourTimer -= Time.deltaTime;
            else
                Lost();

    }

    public void Lost()
    {
        print("NOONONONONONONONONOONO");
        LosePnl.SetActive(true);
        GameManager.instance.isWin = true;//acutaly its not but i enabled it just for stop player movement
    }

    public void ShowKillDillPnl()
    {
        KillDillPnl.gameObject.SetActive(true);
    }
    public void Kill()
    {

        if (Killable)
        {
            killMessegePnl.gameObject.SetActive(true);
        }
    }

    public void Dill()
    {
        Killable = false;

        DillMessegePnl.gameObject.SetActive(true);
    }

    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        WinPnl.SetActive(true);
        GameManager.instance.isWin = true;
    }


    public void GoToFightAngryBoss()
    {
        PlayerController.instance.gameObject.transform.position =
            Vector2.Lerp(PlayerController.instance.gameObject.transform.position, FightPosition.position, 2);
        mainCam.m_Lens.OrthographicSize = 20;
        Boss.SetActive(true);
        psycoHealth.gameObject.SetActive(true);
        psycoHealth.maxValue = _psycoMaxHealht;
        psycoHealth.value = _psycoMaxHealht;
    }

    public void ChanegPsycoHealht(float Amount)
    {
        psycoHealth.value += Amount;
        if (psycoHealth.value <= 0.1f)
            Lost();
    }
}
