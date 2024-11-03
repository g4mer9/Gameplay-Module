using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public string enemyType;
    public int maxHealth;
    public int currentHealth;
    private WaitForSecondsRealtime bloodDuration = new WaitForSecondsRealtime(.1f);
    ParticleSystem.EmissionModule em;

    // Start is called before the first frame update
    void Start()
    {
        if(enemyType == "Jobber")
        {
            maxHealth = 5;
            currentHealth = maxHealth;
        }
        em = GetComponent<ParticleSystem>().emission;
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(BloodEffect());
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator BloodEffect()
    {
        em.enabled = true;
        yield return bloodDuration;
        em.enabled = false;

    }
}
