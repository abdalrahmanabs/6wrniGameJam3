using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    #region Shooting
    [SerializeField] GameObject bullet;
    [SerializeField] Transform ShootingPoint;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtAndShoot();
    }
    void LookAtAndShoot()
    {
        if (!GameManager.instance.isWin)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, ShootingPoint.position, transform.rotation);
                GameManager.instance.PlaySound((int)GameManager.fx.shoot);

            }
        }
    }
}
