using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;


public class Angry : MonoBehaviour
{
    [SerializeField] Slider HealthLevel, AngerLevel;
    public float MaxHealth = 100, damage, MaxAnger = 5, killScore = 30;
    public bool isAnger = false, increaseAnger = true;
    AIPath aiPath;
    public Transform StayPos;

    
    // Start is called before the first frame update
    void OnEnable()
    {
        
        HealthLevel.maxValue = MaxHealth;
        HealthLevel.value = MaxHealth;

        AngerLevel.maxValue = MaxAnger;
        AngerLevel.value = 0;

        aiPath = GetComponent<AIPath>();
        aiPath.canMove = isAnger;

    }

    // Update is called once per frame
    void Update()
    {
        IncreaseAngerLevel();
     
    }

    void IncreaseAngerLevel()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < 35)
        {
            if (AngerLevel.value < MaxAnger && increaseAnger)
            {
                AngerLevel.value += Time.deltaTime;
            }
            else
                StartCoroutine(nameof(DecreaseAngerLevel));


            if (AngerLevel.value >= MaxAnger)
                isAnger = true;
            else
                isAnger = false;
            aiPath.canMove = isAnger;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                break;
            case "Bullet":
                ChangeHealth(-collision.gameObject.GetComponent<BulletMovement>().damage);
                SpawnManager.Instance.DespawnObject(collision.gameObject);
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

    IEnumerator DecreaseAngerLevel()
    {
        yield return new WaitForSeconds(3);
        ReturnToStayPos();
        while (AngerLevel.value > 1)
        {
            increaseAnger = false;
           
            
            AngerLevel.value -= Time.deltaTime * 2;
        }
        increaseAnger = true;
    }
    void ReturnToStayPos()
    {
        transform.position = Vector2.Lerp(transform.position, StayPos.position,1*Time.deltaTime);
    }


   
}
