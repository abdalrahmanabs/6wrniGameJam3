using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager instance;
    public float score;
    [SerializeField] TextMeshProUGUI ScoreTxt;
    
    private void Awake() => instance = this;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTxt.text ="Score: "+ score.ToString();
    }
}
