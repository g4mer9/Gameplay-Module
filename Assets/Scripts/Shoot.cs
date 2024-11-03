using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float gunRange = 50f;
    public AudioSource gunAudio;
    private Camera fpsCam;
    private GameObject player;
    private float nextFire;
    private Light bulletLight;
    private WaitForSecondsRealtime lightDuration = new WaitForSecondsRealtime(.2f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gunAudio = GetComponentInChildren<AudioSource>();
        fpsCam = GetComponentInChildren<Camera>();
        bulletLight = GetComponentInChildren<Light>();
        bulletLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                //uiinfo.ammo--;
                nextFire = Time.time + fireRate;
                ShootGun();
            }
        }
    }

    void ShootGun()
    {
        StartCoroutine(ShotEffect());
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunRange))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                //hit.transform.GetComponent<Enemy>().TakeDamage(gunDamage);
                //enemy takes damage
                throw new NotImplementedException();
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        bulletLight.enabled = true;
        yield return lightDuration;
        bulletLight.enabled = false;

    }
}
