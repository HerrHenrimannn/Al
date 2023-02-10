using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private InputMaster controls;
    public Camera fpsCam;
    public float range = 100f;
    public float damage = 10f;
    [SerializeField]
    private TrailRenderer bulletTrail;
    [SerializeField]
    private Transform bulletStartPoint;
    [SerializeField]
    private ParticleSystem shootingSystem;
    [SerializeField]
    private ParticleSystem impactParticleSystem;
    [SerializeField]
    private LayerMask mask;

    private Animator animator;

    private void Awake()
    {
        controls = new InputMaster();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(controls.Player.Shoot.triggered) ShootGun();
    }
    private void ShootGun()
    {
        RaycastHit hit;
        animator.SetBool("IsShooting", true);
        shootingSystem.Play();
        if(Physics.Raycast(bulletStartPoint.position, fpsCam.transform.forward, out hit, range, mask))
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletStartPoint.position, Quaternion.identity);
            Target target = hit.transform.GetComponent<Target>();
            StartCoroutine(SpawnTrail(trail, hit));
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        animator.SetBool("IsShooting", false);
        trail.transform.position = hit.point;
        Instantiate(impactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
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
