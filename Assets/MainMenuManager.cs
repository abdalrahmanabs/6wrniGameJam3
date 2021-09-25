using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuManager : MonoBehaviour
{


    public GameObject[] Pnls;
    public Button Music, Sound;

 

    //0 = SettingsPnl
    //1 = AboutPnl
    //2= FullMainMenu
    //3=story
    // Start is called before the first frame update

    public Slider VoiceSlider;
    void Start()
    {
     
        SaveManager.LoadData();
        if (GameManager.instance.data.Music)
        {
           
            Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "مشغل";
            Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;

        }

        else
        {
           
            Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "مطفأ";
            Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }

        if (GameManager.instance.data.SoundEffects)
        {
           

            Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "مشغل";
            Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;

        }

        else
        {

            
            Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "مطفأ";
            Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

        }

    }

    // Update is called once per frame
 

    public void ButtonController(string ButtonName)
    {
        switch (ButtonName)
        {
            case "Settings":
                LoadPnl(0);
                break;
            case "CloseGame":
                Application.Quit();
                break;
 
            case "Back":
                LoadPnl(1);
                break;
            case "Music":
                if (GameManager.instance.data.Music)
                {
                   
                    Music.gameObject.transform.GetComponentInChildren<ArabicFixerTMPRO>().fixedText = "مطفأ";
                    Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    GameManager.instance.data.Music = false;

                }
                else
                {

                    
                    Music.gameObject.transform.GetComponentInChildren<ArabicFixerTMPRO>().fixedText = "مشغل";
                    Music.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    GameManager.instance.data.Music = true;
                }
                break;
            case "Sound":

                if (GameManager.instance.data.SoundEffects)
                {
                  
                    Sound.gameObject.transform.GetComponentInChildren<ArabicFixerTMPRO>().fixedText = "مطفأ";
                    Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    GameManager.instance.data.SoundEffects = false;
                }
                else
                {

                    Sound.gameObject.transform.GetComponentInChildren<ArabicFixerTMPRO>().fixedText = "مشغل";
                    Sound.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    GameManager.instance.data.SoundEffects = true;
                }
                break;

           

           

            case"Story":
                LoadPnl(3);
                break;
            case "Buttons":
                LoadPnl(2);
                break;



        }
        SaveManager.Save();
       // GameManager.instance.PlaySound(4);

    }


    public void LoadPnl(int pnlIndex)
    {
        foreach (var i in Pnls)
            i.gameObject.SetActive(false);

        Pnls[pnlIndex].gameObject.SetActive(true);
    }
}


