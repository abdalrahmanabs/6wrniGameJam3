using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    [SerializeField] Slider HealthLevel, AngerLevel;
    public float MaxHealth = 100, damage, MaxAngerTime, killScore = 30, waitTime;
    public bool isAnger = false, increaseAnger = true;
    AIPath aiPath;
    bool PlayAnimation;
    Animator anim;
    string CurrentState = "";
    string[] AnimationsNames = new string[2] { "idle 0", "Angry" };
    public Transform StayPos;

    // Start is called before the first frame update
    void OnEnable()
    {

        HealthLevel.maxValue = MaxHealth;
        HealthLevel.value = MaxHealth;

        AngerLevel.maxValue = MaxAngerTime;
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
        aiPath.canMove = isAnger;

        print("increase State is _" + increaseAnger);

        PlayAnimation = true;
        if (AngerLevel.value < MaxAngerTime && increaseAnger)
        {
            print("sh76 Increasing anger Level");
            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
            if (!increaseAnger)
                return;
            else
            {

                AngerLevel.value += Time.deltaTime;
            }


        }
        else if (AngerLevel.value >= MaxAngerTime || !increaseAnger)
        {

            isAnger = true;

            GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.Angry], ref CurrentState);
            increaseAnger = false;
            if (AngerLevel.value >= 0.1f)
                AngerLevel.value -= Time.deltaTime;

            else
            {
                print("Shit");
                increaseAnger = true;
                GameManager.instance.PlayAnimation(anim, AnimationsNames[(int)AnimationsIDs.idle], ref CurrentState);
                isAnger = false;
                ReturnToStayPos();
            }
        }

        else
            PlayAnimation = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":

                break;
            case "Bullet":
                ChangeHealth(-collision.gameObject.GetComponent<BulletMovement>().damage);
                SpawnManager.Instance.DespawnObject(collision.gameObject);
                break;

        }

    }

    public void ChangeHealth(float val)
    {
        HealthLevel.value += val;
        if (HealthLevel.value <= 0)
            Death();
    }

    void Death()
    {
        GameManager.instance.PlaySound((int)GameManager.fx.die);

        GameManager.instance.TotalScore += killScore;
        Level2Manager.instance.score += killScore;
       // Level2Manager.instance.isWin = true;
        Level2Manager.instance.Portal.SetActive(true);

        gameObject.SetActive(false);
    }

  

    void ReturnToStayPos()
    {
        transform.position = Vector2.Lerp(transform.position, StayPos.position, 1 * Time.deltaTime );
    }

}
