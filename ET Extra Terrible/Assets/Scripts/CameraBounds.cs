using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bound
{
    bottom,
    left,
    right,
    top
}

public class CameraBounds : MonoBehaviour
{
    
    public Bound bound;
    public bool isColliding = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Environment should be labeled as far left and right most colliders are walls, colliders at the very bottom as ground and colliders at the very top are ceiling
        if (collision.tag == "Wall" && (bound == Bound.left || bound == Bound.right))
        {
            isColliding = true;
        }
        else if (collision.tag == "Ground" && bound == Bound.bottom)
        {
            isColliding = true;
        }
        else if (collision.tag == "Ceiling" && bound == Bound.top)
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
        
        //Debug.Log(collision.tag);
    }
}
