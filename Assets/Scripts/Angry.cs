using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;


public enum AnimationsIDs
{
   idle=0,Angry=1
}

public class Angry : MonoBehaviour
{
    [SerializeField] Slider HealthLevel, AngerLevel;
    public float MaxHealth = 100, damage, MaxAnger , killScore = 30,waitTime;
    public bool isAnger = false, increaseAnger = true;
    AIPath aiPath;
    public Transform StayPos;
    bool PlayAnimation;
    Animator anim;
    string CurrentState = "";
    string[] AnimationsNames = new string[2] { "idle 0", "Angry" };

    
    // Start is called before the first frame update
    void OnEnable()
    {
        
        HealthLevel.maxValue = MaxHealth;
        HealthLevel.value = MaxHealth;

        AngerLevel.maxValue = MaxAnger;
        AngerLevel.value = 0;

        aiPath = GetComponent<AIPath>();
        aiPath.canMove = isAnger;

        anim = GetComponent<Animator>();

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
            PlayAnimation = true;
            if (AngerLevel.value < MaxAnger && increaseAnger)
            {
                AngerLevel.value += Time.deltaTime;
            }
            else
                StartCoroutine(nameof(DecreaseAngerLevel));


            if (AngerLevel.value >= MaxAnger)
            {
                isAnger = true;

                GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.Angry], ref CurrentState);
            }
            else
            {
                isAnger = false;
                GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
            }
            aiPath.canMove = isAnger;
        }
        else
            PlayAnimation = false;
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
        yield return new WaitForSeconds(waitTime);
        if(PlayAnimation)
             GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
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
