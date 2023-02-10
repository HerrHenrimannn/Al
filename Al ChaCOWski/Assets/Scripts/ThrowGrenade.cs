using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowGrenade : MonoBehaviour
{
    private InputMaster controls;
    private float grenadecount = 3f;
    public GameObject grenade;
    public float throwForce = 5f;
    public Transform fpsCam;
    public Transform attackPoint;
    private void Awake()
    {
        controls = new InputMaster();
    }
    private void Update()
    {
        if(grenadecount > 0 && controls.Player.Grenade.triggered)
        {
            Yeet();
        }
    }
    private void Yeet()
    {
        Debug.Log("Yeet");
        GameObject projectile = Instantiate(grenade, attackPoint.position, fpsCam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 force = transform.forward * throwForce + transform.up * throwForce;

        projectileRb.AddForce(force, ForceMode.Impulse);
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
