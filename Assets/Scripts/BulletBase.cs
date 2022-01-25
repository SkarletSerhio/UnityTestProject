using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    private float bulletSpeed;

    public void BulletSpawn(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
}
