using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager instance;
    public float score;
    [SerializeField] TextMeshProUGUI ScoreTxt;
    [SerializeField] Slider psycoHealth;
    float _psycoMaxHealht=100;
    public float currPsycoHealth;
    private void Awake() => instance = this;

    // Start is called before the first frame update
    void Start()
    {
        psycoHealth.maxValue = _psycoMaxHealht;
        psycoHealth.value = _psycoMaxHealht;
    }

    // Update is called once per frame
    void Update()
    {
        currPsycoHealth = psycoHealth.value;
        ScoreTxt.text ="Score: "+ score.ToString();

        if (currPsycoHealth <= 0)
            Lost();
    }

    public void ChangePsycoHealth(float amount)
    {
        psycoHealth.value += amount;
    }

    public void Lost()
    {
        print("sh76 you lost");
    }
}
