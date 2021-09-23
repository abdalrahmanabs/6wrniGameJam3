using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Angry : MonoBehaviour
{
    [SerializeField] Slider HealthLevel, AngerLevel;
    public float MaxHealth = 100, damage, MaxAnger = 30, killScore = 30;
    // Start is called before the first frame update
    void OnEnable()
    {
        HealthLevel.maxValue = MaxHealth;
        HealthLevel.value = MaxHealth;

        AngerLevel.maxValue = MaxAnger;
        AngerLevel.value = MaxAnger;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseAngerLevel();
        
    }

    void IncreaseAngerLevel()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                break;
            case "Bullet":
                ChangeHealth(-collision.gameObject.GetComponent<BulletMovement>().damage);
                collision.gameObject.SetActive(false);
                break;

        }

    }

    void ChangeHealth(float val)
    {
        HealthLevel.value += val;
        if (HealthLevel.value <= 0)
            Death();
    }

    void Death()
    {
        GameManager.instance.TotalScore += killScore;
        Level1Manager.instance.score += killScore;


        gameObject.SetActive(false);
    }
}
