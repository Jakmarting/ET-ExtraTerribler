using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Laser projectile;
    public Transform shootPoint;
    public float speed;
    public float spread;
    public int ammo;
    public int maxAmmo;
    public int ammoReg;

    private int regenMod = 0;
    private GameObject weaponHolder;
    private void Start()
    {
        ammo = maxAmmo;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    public void Fire(string dir)
    {
        if (ammo > 0)
        {
            Laser newLaser = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            newLaser.SetAngle(weaponHolder.transform.rotation.z + Random.Range(-spread, spread));
            if (dir == "right") newLaser.turn();
            ammo -= newLaser.ammoLoss;
        }
        else
        {
            Debug.Log("Not enough ammo");
        }
    }
    public void Regen()
    {
        // Debug.Log(ammo);
        if (ammoReg > regenMod)
        {
            if (ammo < maxAmmo) ammo += ammoReg + regenMod;
        }

        if (regenMod > 0) regenMod--; 
        else if (regenMod < 0) regenMod++;
    }
    public void ChangeRegen(int amt)
    {
        if (Mathf.Abs(regenMod) > 100) Debug.Log("Stalled too much");
        else regenMod += amt;
    }
}
