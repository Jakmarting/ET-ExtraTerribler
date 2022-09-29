using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /* Currently getting an error/message because the loading scene has a camera as well */

    private Rigidbody2D rb;

    float horizontal;
    float vertical;

    float jumpForce = 15f;
    float walkModifier = 5f;
    string facing = "left";
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    int ground_timer = 0;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public GameObject weaponHolder;
    private Weapon weapon;

    private float shootTimeCounter;
    public float shootTime;
    private bool isShooting;

    public GameObject throwObject;
    private float throwForce = 10f;

    public float maxHealth;
    public float health;
    public HealthBar healthBar;

    public float maxAmmo;
    public float ammo;
    public AmmoBar ammoBar;

    private List<Piece> pieces = new List<Piece>();

    void DoInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        // Movement
        if(isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter-= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }


        if (weapon)
        {
            // Shooting
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (weapon.ammo > 0)
                {
                    isShooting = true;
                    shootTimeCounter = shootTime;
                    weapon.Fire(facing);
                    // rb.velocity = Vector2.up * jumpForce; DEBUG
                }
                else
                {
                    Debug.Log("Stalled");
                    weapon.ChangeRegen(-10);
                }
            }
            if (Input.GetKey(KeyCode.Space) && isShooting)
            {
                if (shootTimeCounter > 0)
                {
                    weapon.Fire(facing); // this will need to use the fact that it is measuring how long the user has pressed space for
                                   // rb.velocity = Vector2.up * jumpForce; DEBUG
                    shootTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isShooting = false;
                }

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isShooting = false;
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                weapon.Regen();
            }

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DoThrow(throwObject);
        }
    }

    void DoRotation()
    {
        if (horizontal < 0 && facing.Equals("left"))
        {
            //spriteRenderer.flipX = true;
            transform.Rotate(0f, 180f,0f);
            facing = "right";
        }
        else if(horizontal > 0 && facing.Equals("right"))
        {
            //spriteRenderer.flipX = false;
            transform.Rotate(0f, 180f, 0f);
            facing = "left";
        }
        
    }

    void DoMovement()
    {
        this.transform.position += new Vector3(horizontal * walkModifier,0,0) * Time.deltaTime;
    }


    void DoGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }


    void DoThrow(GameObject thr)
    {
        int dir = 1;
        if (facing == "right")
        {
            dir = -1;
        }
        Vector3 spawnPos = new Vector3(transform.position.x + dir, transform.position.y, 0f);
        GameObject throwO = Instantiate(thr, spawnPos,transform.rotation);
        throwO.GetComponent<Rigidbody2D>().velocity = new Vector3(dir, 0f, 0f) * throwForce;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            Debug.Log("Player dead");
        }
        healthBar.SetHealth((int)(health));
    }


    // What to do if the player dies
    void PlayerDead()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collect")
        {
            Weapon getWeapon = collision.gameObject.GetComponentInChildren<Weapon>();
            if (getWeapon)
            {

                if (weaponHolder.transform.childCount > 0)
                {
                    GameObject oldGameObject = weaponHolder.GetComponentInChildren<Weapon>().gameObject;
                    //Debug.Log("GameObject:\t"+oldGameObject);
                    if (oldGameObject)
                    {
                        Destroy(oldGameObject);
                    }
                }
                weapon = Instantiate(getWeapon, weaponHolder.transform);
                //Debug.Log("Weapon:\t"+getWeapon);
                Destroy(getWeapon.GetComponentInParent<Rigidbody2D>().gameObject);
                //Destroy(getWeapon.gameObject);
            }
            else
            {
                Piece piece = collision.gameObject.GetComponentInChildren<Piece>();
                if (piece)
                {
                    pieces.Add(piece);
                }
                Destroy(piece.GetComponentInParent<Rigidbody2D>().gameObject);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponentInChildren<Enemy>();
            if (enemy) TakeDamage((int)enemy.meleeDamage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        weapon = weaponHolder.GetComponentInChildren<Weapon>();
        health = maxHealth;

        healthBar.SetMaxHealth((int)(maxHealth));
        healthBar.SetHealth((int)(health));
    }

    // Update is called once per frame
    void Update()
    {
        DoInput();
        DoRotation();
        DoMovement();
        DoGrounded();
    }
}
