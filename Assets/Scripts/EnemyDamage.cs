using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public string enemyType;
    public int maxHealth;
    public int currentHealth;
    private SpawnLoot loot;


    // Start is called before the first frame update
    void Start()
    {
        loot = GetComponent<SpawnLoot>();
        if (enemyType == "Jobber")
        {
            maxHealth = 5;
            currentHealth = maxHealth;
        }
        else if (enemyType == "Brute")
        {
            maxHealth = 15;
            currentHealth = maxHealth;
        }
    }
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth == 0)
        {
            loot.SpawnRandomItem();
            gameObject.SetActive(false);
        }
    }

}
