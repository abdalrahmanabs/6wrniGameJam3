using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float ShootingSpeed, damage=50;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(Death), 4);
        if (SceneManager.GetActiveScene().name == "Level 2")
            ShootingSpeed *=1.5f;
    }

    // Update is called once per frame
    void Update()
    {   
        rb.velocity= transform.right * ShootingSpeed * Time.deltaTime ;
    }
    void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "TopBoss":
                
                collision.gameObject.GetComponent<BossController>().ChangeHealth((-damage) * 2);
                gameObject.SetActive(false);
                break;
            case"Boss":
                collision.gameObject.GetComponent<BossController>().ChangeHealth((-damage));
                gameObject.SetActive(false);
                break;
        }
    }
}
