using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
<<<<<<< HEAD

    public float health = 200f;
    public Vector3 position;

=======
    public int maxHealth = 100;
    public int currentHealth;


    public HealthBar healthBar;
>>>>>>> bryce

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void takeDamage(float damage){
        health -= damage;
        if(health <= 0){
            //Die
        }
    }
}
