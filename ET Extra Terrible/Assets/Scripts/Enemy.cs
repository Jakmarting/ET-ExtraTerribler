using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject followTarget;

    public float meleeDamage;
    public float runSpeed;
    public float walkSpeed;

    public float maxHealth;
    public float armour;
    public List<Shield> shields = new List<Shield>();

    public float followRange;
    public float chaseCounter;

    private float chaseCount;
    private bool chasing;

    private string direction = "right";
    private int move = 1;
    private float currentHealth;
    private float moveSpeed;

    void DoLook()
    {
        if (Vector2.Distance(this.gameObject.transform.position, followTarget.transform.position) < followRange)
        {
            if (chasing)
            {
                if (this.gameObject.transform.position.x < followTarget.transform.position.x && direction.Equals("left"))
                {
                    transform.Rotate(0f, 180f, 0f);
                    direction = "right";
                    move *= -1;
                }
                else if (this.gameObject.transform.position.x > followTarget.transform.position.x && direction.Equals("right"))
                {
                    transform.Rotate(0f, 180f, 0f);
                    direction = "left";
                    move *= -1;
                }
            }
            else if ((this.gameObject.transform.position.x < followTarget.transform.position.x && direction.Equals("left")) || (this.gameObject.transform.position.x > followTarget.transform.position.x && direction.Equals("right")))
            {
                chaseCount -= Time.deltaTime;
            }
            else
            {
                chasing = true;
                moveSpeed = runSpeed;
                chaseCount = chaseCounter;
            }
        }
        else if (chasing)
        {
            chaseCount -= Time.deltaTime;
        }

        if (chaseCount < 0)
        {
            chaseCount = 0;
            chasing = false;
            moveSpeed = walkSpeed;
        }
    }

    void DoMove()
    {
        transform.position += new Vector3(move, 0, 0) * Time.deltaTime * moveSpeed;
    }

    public void Damage(float dmg)
    {
        if (shields.Count > 0)
        {
            List<Shield> remShields = new List<Shield>();
            foreach (Shield s in shields)
            {
                s.shieldHealth -= dmg;
                if (s.shieldHealth < 0)
                {
                    remShields.Add(s);
                }
            }
            foreach (Shield rs in remShields)
            {
                shields.Remove(rs);
            }
        }
        else
        {
            if (dmg > armour) currentHealth -= (dmg - armour);
            Debug.Log(this.gameObject.tag + " has " + currentHealth + "hp");
            if (currentHealth < 0) Destroy(this.gameObject); // spawn the head explode thing
        }
    }

    public string GetDir()
    {
        return direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (!followTarget)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(this.gameObject.transform.position.x - followTarget.transform.position.x) > 1)
        {
            DoLook();
            DoMove();
        }
    }
}
