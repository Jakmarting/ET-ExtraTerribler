using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    public Effect effect;
    public float fuse;

    private float expand = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fuse -= Time.deltaTime;
        expand += (Time.deltaTime/4 + Mathf.Sin(Time.deltaTime)/10);
        transform.localScale = new Vector3(expand,expand,0);
        if (fuse < 0)
        {
            if (explosion) Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
