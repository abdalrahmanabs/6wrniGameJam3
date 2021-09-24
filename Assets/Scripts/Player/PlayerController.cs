using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public static PlayerController instance;
    Animator animator;
    Camera cam;
    #region CollectObjects
    float MaxCollectables, CurrentCollictables, CollectScore;
    [SerializeField] TextMeshProUGUI Collecteds;
    #endregion

    #region movementVars
    Rigidbody2D rb;
    bool isGrounded = true;
    [SerializeField]
    float PlayerSpeed, JumpSpeed, xAxis;
    #endregion

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        if (!Level2Manager.instance.isWin)
            xAxis = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xAxis * PlayerSpeed * Time.fixedDeltaTime, rb.velocity.y);
        //jump
        if (Input.GetAxis("Jump") > 0.01 && isGrounded)
        {
            print("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, 1 * Time.fixedDeltaTime * JumpSpeed); ;
            isGrounded = false;
        }


        //sprint
        if (Input.GetKey(KeyCode.LeftShift))
            PlayerSpeed = 700;
        else
            PlayerSpeed = 500;
    }


    void Collect(GameObject CollectableObject)
    {
        if (!Level1Manager.instance.isWin)
            print("Collecting");
        SpawnManager.Instance.DespawnObject(CollectableObject);
        CurrentCollictables++;
        Level1Manager.instance.score += 30;
        Level1Manager.instance.ChangePsycoHealth(8);
        Collecteds.text = "Collected: " + CurrentCollictables.ToString() + " / " + MaxCollectables.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                print("Grounded");
                isGrounded = true;
                break;

            case "Angry":
                print("anger Joined");
                isGrounded = true;
                if (!collision.gameObject.GetComponent<Angry>().isAnger)
                {
                    if (CurrentCollictables >= MaxCollectables)
                        return;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Collect(collision.gameObject);

                    }
                }
                else
                {
                    print("is ANger go go go ");
                    Level1Manager.instance.ChangePsycoHealth(-collision.gameObject.GetComponent<Angry>().damage);
                }

                break;
            case "Portal":
                Level2Manager.instance.isWin = true;
                Level1Manager.instance.Win();
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Angry":
                if (collision.gameObject.GetComponent<Angry>().isAnger)
                {
                    Level1Manager.instance.ChangePsycoHealth(-0.09f);
                    print("sh76");
                }
                else
                {
                    print("not sh76");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        print("collecting");
                        Collect(collision.gameObject);
                        Level1Manager.instance.ChangePsycoHealth(20);
                    }
                }
                break;
            case "NegativePlatform":
                Level2Manager.instance.ChanegPsycoHealht(-0.03f);
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

      
        switch(collision.gameObject.tag)
        {
            case "Portal":
                Level2Manager.instance.Win();
                break;
            case "Boss":
                if (collision.gameObject.name != "Boss" || !collision.gameObject.GetComponent<BossController>().isAnger)
                    return;
                Level2Manager.instance.ChanegPsycoHealht(-10);

                break;
        }
   

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Stoler":
                Level2Manager.instance.ShowKillDillPnl();
                Level2Manager.instance.finishParckour = true;
                break;
            case "Boss":
                if (collision.gameObject.name != "Boss"||!collision.gameObject.GetComponent<BossController>().isAnger)
                    return;

                Level2Manager.instance.ChanegPsycoHealht(-1.3f);
                break;
        }
    }

    
}
