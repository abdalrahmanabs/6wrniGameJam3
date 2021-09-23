using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float ShootingSpeed, damage=50;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(transform.position.x,transform.position.y)* ShootingSpeed * Time.deltaTime;
    }
}
