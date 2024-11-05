using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public GameObject player;
    private UIInfo ui_instance;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        ui_instance = player.GetComponent<PlayerController>().uiInfo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ui_instance.ammo += 10;
            Destroy(gameObject);
        }
    }

}
