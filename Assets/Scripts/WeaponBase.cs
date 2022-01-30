using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{

    public float Damage, Range, BulletSpeed, ShootDelay;
    [SerializeField]public GameObject Bullet, BulletSpawner;

    float timePass = 0f;

    private void Awake()
    {
        
    }
    
    public virtual void Shoot()
    {
        if (Time.time >= timePass)
        {
            GameObject bulletInstance = Instantiate<GameObject>(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
            bulletInstance.GetComponent<BulletBase>().BulletSpawn(BulletSpeed);
            Destroy(bulletInstance, Range/10);
            timePass = Time.time + ShootDelay/20;
        } 
        
    }

}
