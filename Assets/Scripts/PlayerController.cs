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
    float MaxCollectables, CurrentCollictables,CollectScore;
    [SerializeField]TextMeshProUGUI Collecteds;
    #endregion

    #region movementVars
    Rigidbody2D rb;
    bool isGrounded=true;
    [SerializeField]
    float PlayerSpeed = 200, JumpSpeed, xAxis;
    #endregion
    #region Shooting
    [SerializeField] GameObject bullet;
    [SerializeField] Transform ShootingPoint,weapon;
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
        LookAtAndShoot();
    }

    void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
       
        rb.velocity = new Vector2(xAxis * PlayerSpeed*Time.fixedDeltaTime, rb.velocity.y);
        //jump
        if(Input.GetAxis("Jump")>0.01&&isGrounded)
        {
            print("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, 1 * Time.fixedDeltaTime * JumpSpeed); ;
            isGrounded = false;
        }


        //sprint
        if (Input.GetKey(KeyCode.LeftShift))
            PlayerSpeed = 300;
        else
            PlayerSpeed = 200;
    }
   
    void LookAtAndShoot()
    {
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position ;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0,0,angle);
       
        if (Input.GetMouseButtonDown(0))
        {
            SpawnManager.Instance.SpawnObject(bullet);
            bullet.transform.position = ShootingPoint.transform.position;
        }
    }
    void Collect(GameObject CollectableObject)
    {
        print("Collecting");
        SpawnManager.Instance.DespawnObject(CollectableObject);
        CurrentCollictables++;
        Level1Manager.instance.score += 30;
        Level1Manager.instance.ChangePsycoHealth(8);
        Collecteds.text= "Collected: "+CurrentCollictables.ToString() + " / "+MaxCollectables.ToString() ;
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
                    Level1Manager.instance.ChangePsycoHealth(-20);
                }

                    break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
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
        }
    }


}
