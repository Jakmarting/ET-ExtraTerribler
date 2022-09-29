using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossETB : MonoBehaviour
{
    public Weapon weapon;
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (count > 30)
        {
            string d = GetComponent<Enemy>().GetDir();
            if (d == "left") d = "right";
            else d = "left";
            weapon.Fire(d);
            count = 0;
        }
        count++;

    }
}
