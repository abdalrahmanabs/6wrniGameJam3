using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;



public class Angry : MonoBehaviour
{
    public enum Animations
    {
        idle = 0, Angry = 1
    }

    [SerializeField] Slider HealthLevel, FearLevel;
    public float MaxHealth = 100, damage, MaxAnger , killScore = 30,waitTime;
    public bool isFearFull = false, increaseFear = true;
    AIPath aiPath;
   
    bool PlayAnimation;
    Animator anim;
    string CurrentState = "";
    string[] AnimationsNames = new string[2] { "idle 0", "Angry" };

    
    // Start is called before the first frame update
    void OnEnable()
    {
        
        HealthLevel.maxValue = MaxHealth;
        HealthLevel.value = MaxHealth;

        FearLevel.maxValue = MaxAnger;
        FearLevel.value = 0;

        aiPath = GetComponent<AIPath>();
        aiPath.canMove = isFearFull;

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        IncreaseAngerLevel();
     
    }

    //void IncreaseAngerLevel()
    //{
    //    if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < 35)
    //    {
    //        PlayAnimation = true;
    //        if (AngerLevel.value < MaxAnger && increaseAnger)
    //        {
    //            AngerLevel.value += Time.deltaTime;
    //        }
    //        else
    //            StartCoroutine(nameof(DecreaseAngerLevel));


    //        if (AngerLevel.value >= MaxAnger)
    //        {
    //            isAnger = true;

    //            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.Angry], ref CurrentState);
    //        }
    //        else
    //        {
    //            isAnger = false;
    //            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
    //        }
    //        aiPath.canMove = isAnger;
    //    }
    //    else
    //        PlayAnimation = false;
    //}

    void IncreaseAngerLevel()
    {
        aiPath.canMove = isFearFull;

        print("increase State is _" + increaseFear);

        PlayAnimation = true;
        if (FearLevel.value < MaxAnger && increaseFear)
        {
            print("sh76 Increasing anger Level");
            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
            if (!increaseFear)
                return;
            else
            {

                FearLevel.value += Time.deltaTime;
            }


        }
        else if (FearLevel.value >= MaxAnger || !increaseFear)
        {

            isFearFull = true;

            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.Angry], ref CurrentState);
            increaseFear = false;
            if (FearLevel.value >= 0.1f)
                FearLevel.value -= Time.deltaTime;

            else
            {
                print("Shit");
                increaseFear = true;
                GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
                isFearFull = false;
                
            }
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

 
  

   
}
