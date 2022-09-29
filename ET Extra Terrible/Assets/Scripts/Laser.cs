using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public string type;
    public GameObject impact;
    public float laserDamage;
    public int ammoLoss;

    private float laserLife = 1f;
    private float laserSpeed = 10f;
    private float angle = 0f;
    
    public void SetSpeed(float s)
    {
        laserSpeed = s;
    }
    public void SetAngle(float a)
    {
        angle = a;
    }
    public void turn()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void DoMovement()
    {
        transform.Translate(Quaternion.Euler(0, 0, angle) * new Vector3(laserSpeed, 0, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if(collision.tag == "Enemy")
        {
            if (impact == null) return;
            collision.gameObject.GetComponent<Enemy>().Damage(laserDamage);
            Instantiate(impact, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Ground")
        {
            Instantiate(impact, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Debug.Log(transform.position);
        Destroy(gameObject, laserLife);
    }

    void Update()
    {
        DoMovement();
    }
}
