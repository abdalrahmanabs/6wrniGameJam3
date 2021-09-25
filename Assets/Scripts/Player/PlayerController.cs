using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public static PlayerController instance;
    Animator animator;
    float DefaultRigidBodyGravity = 0;
    float DefaultCamSize, maxCamSize;
    Camera camera;
    bool isZoomed=false,isZoomedFinished=false;
   [SerializeField] CinemachineVirtualCamera cam;
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
        //DefaultCamSize = cam.m_Lens.OrthographicSize;
        switch (SceneManager.GetActiveScene().name)
        { case "Level 1":maxCamSize = DefaultCamSize + 5; break;
            case "Level 2": maxCamSize = DefaultCamSize + 9; break;
            case "Level 3": maxCamSize = DefaultCamSize +5  ; break;
            case "Level 4": maxCamSize = DefaultCamSize + 8; break;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera= Camera.main;
        DefaultRigidBodyGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("z") && !isZoomedFinished)
        {
            isZoomed = !isZoomed;
            isZoomedFinished = false;
        }
        Move();
        ////zoom in
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (isZoomed && !isZoomedFinished)
        //        zoomIN();
        //}
        ////zoom out
        //if (Input.GetKeyUp(KeyCode.C) )
        //{
        //    if (!isZoomed && !isZoomedFinished)
        //        zoomOut();
        //}

      

    }

    void Move()
    {
        if (!GameManager.instance.isWin)
        {
            xAxis = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(xAxis * PlayerSpeed * Time.fixedDeltaTime, rb.velocity.y);
            //jump
            if (Input.GetAxis("Jump") > 0.01 && isGrounded)
            {
                print("Jumping");
                rb.velocity = new Vector2(rb.velocity.x, 1 * Time.fixedDeltaTime * JumpSpeed); ;
                isGrounded = false;
            }

            if (Input.GetKey(KeyCode.LeftControl))
                rb.gravityScale = 3.5f;
            else
                rb.gravityScale = 1.5f;
            //sprint
            if (Input.GetKey(KeyCode.LeftShift))
                PlayerSpeed = 700;
            else
                PlayerSpeed = 500;
        }
    }
    


    float t;
    void zoomIN()
    {
        t += 1 * Time.deltaTime;
        //Cam size
        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, DefaultCamSize, t);
        // is animation over ?
        if (t >= 1)
        {
            isZoomedFinished = true;
            t = 0;
        }
    }
    void zoomOut()
    {
        t += 1 * Time.deltaTime;

        //Cam size
        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, maxCamSize, t);

   
        // is animation over ?
        if (t >= 1)
        {
            isZoomedFinished = true;
            t = 0;
        }
    }

    void Collect(GameObject CollectableObject)
    {
        if (!GameManager.instance.isWin)
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
                GameManager.instance.isWin = true;
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Level 1": Level1Manager.instance.Win(); break;
                    case "Level 2": Level2Manager.instance.Win(); break;
                    case "Level 3": Level3Manager.instance.Win(); break;
                }

                break;

            

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "MovingPlatform":
                print("closing platforms ");
                transform.parent = null;
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
            case "Fear":
                Level3Manager.instance.ChangePsycoHealth(-0.5f);
                break;
            case "MovingPlatform":
                isGrounded = true;
                transform.parent = collision.gameObject.transform;
                break;
           

                break;


        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        switch (collision.gameObject.tag)
        {
            case "Portal":
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Level 1": Level1Manager.instance.Win(); break;
                    case "Level 2": Level2Manager.instance.Win(); break;
                    case "Level 3": Level3Manager.instance.Win(); break;
                }
                break;
            case "Boss":
                if (collision.gameObject.name != "Boss" || !collision.gameObject.GetComponent<BossController>().isAnger)
                    return;
                Level2Manager.instance.ChanegPsycoHealht(-collision.gameObject.GetComponent<Fear>().damage);

                break;
            case "Fear":
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                break;

        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Pickup":
                GameObject obj= collision.gameObject;
                print("Joined your channel");
                if (Input.GetMouseButton(1))
                {

                    Pickup(ref obj);
                    print("Hello tekaseem");
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    print("Bye tekaseem");
                    drop(ref obj);
                }
                else
                {
                    print("Bye tekaseem");
                    drop(ref obj);
                }
                break;
            case "Stoler":
                Level2Manager.instance.ShowKillDillPnl();
                Level2Manager.instance.finishParckour = true;
                break;
            case "Boss":
                if (collision.gameObject.name != "Boss" || !collision.gameObject.GetComponent<BossController>().isAnger)
                    return;

                Level2Manager.instance.ChanegPsycoHealht(-1.3f);
                break;

            case "Fear":
                Level3Manager.instance.ChangePsycoHealth(-0.5f);
                break;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fear":
                StartCoroutine(EndFear());
                break;
            case "Picup":
                GameObject obj = collision.gameObject;
                drop(ref obj);
                break;
        }
    }

    IEnumerator EndFear()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }

    void Pickup(ref GameObject obj)
    {

        obj.GetComponentInParent<Rigidbody2D>().gameObject.transform.SetParent(transform);
        obj.GetComponentInParent<Rigidbody2D>().isKinematic = true ;
        rb.gravityScale = DefaultRigidBodyGravity + 1;
    }
    void drop(ref GameObject obj)
    {

        obj.GetComponentInParent<Rigidbody2D>().gameObject.transform.SetParent(null);
        obj.GetComponentInParent<Rigidbody2D>().isKinematic = false;
        rb.gravityScale = DefaultRigidBodyGravity ;
    }

    
}
