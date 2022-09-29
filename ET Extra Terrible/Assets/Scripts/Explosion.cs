using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float expand = 0.3f;
    float expandSpeed = 10f;
    float explosionDamage = 10f;

    // Update is called once per frame
    void Update()
    {
        expand += Time.deltaTime * expandSpeed;
        transform.localScale = new Vector3(1f,1f, 0f) * expand;

        if (expand > 3f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(explosionDamage);
        }
    }
}
