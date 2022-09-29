using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWith : MonoBehaviour
{
    public LayerMask LayerToIgnore;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerToIgnore)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
