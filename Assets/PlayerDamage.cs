using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private UIInfo ui_instance;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui_instance = other.gameObject.GetComponent<PlayerController>().uiInfo;
            ui_instance.health -= 10 * Time.deltaTime;
        }
    }
}

