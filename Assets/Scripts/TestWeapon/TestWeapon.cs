using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeapon : WeaponBase
{
    Controlls controlls;
    
    void Awake()
    {
        controlls = new Controlls();

        controlls.Character.Attack.performed += onShoot;
        controlls.Character.Attack.canceled += onShoot;
        controlls.Character.Attack.Enable();
    }

    void onShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

}
