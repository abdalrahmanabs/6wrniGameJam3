using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [HideInInspector]
    public static PlayerController instance;
    Animator animator;
    Camera cam;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                print("Grounded");
                isGrounded = true;
                break;
        }
    }
    void LookAtAndShoot()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle, -90,90);
        weapon.transform.rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
       
        if (Input.GetMouseButtonDown(0))
        {
            SpawnManager.Instance.SpawnObject(bullet);
            bullet.transform.position = ShootingPoint.transform.position;
        }
    }
    void Collect()
    {

    }


}
