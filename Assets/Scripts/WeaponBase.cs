using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    Controlls controlls;

    public float Damage, Range, BulletSpeed, ShootDelay;
    [SerializeField]public GameObject WeaponModel, Bullet, BulletSpawner;

    private void Awake()
    {
        
    }
    
    public virtual void Shoot()
    {
        GameObject bulletInstance = Instantiate<GameObject>(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
        bulletInstance.GetComponent<BulletBase>().BulletSpawn(BulletSpeed);
        Destroy(bulletInstance, Range);

    }

}
