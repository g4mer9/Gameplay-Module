using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private UIInfo ui_instance;
    private WaitForSecondsRealtime lightDuration = new WaitForSecondsRealtime(.2f);
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ui_instance = player.GetComponent<PlayerController>().uiInfo;
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
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ui_instance.ammo > 0)
            {
                ui_instance.ammo--;
                nextFire = Time.time + fireRate;
                ShootGun();
            }
        }
    }

    void ShootGun()
    {
        StartCoroutine(ShotEffect());
        //RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, gunRange);

        foreach (RaycastHit hit in hits)
        {
            // Check if the hit object has a Renderer component
            if (hit.collider.gameObject.GetComponent<Renderer>() != null && hit.transform.tag == "Enemy")
            {
                Debug.Log(hit.transform.name);
                EnemyDamage enemyDamage = hit.collider.GetComponent<EnemyDamage>();

                if (enemyDamage != null)
                {
                    // Call the Damage() method if the component exists
                    enemyDamage.Damage(1);
                    Debug.Log("Enemy Hit");
                }
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
