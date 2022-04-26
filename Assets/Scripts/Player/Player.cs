using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;
    public Vector3 position;


    public HealthBar healthBar;


    private Animator animator;
    CursorLockMode lockMode;
    // Start is called before the first frame update
    void Start()
    {
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = gameObject.GetComponent<Animator>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(20);
        }
    }



    public void takeDamage(float damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            //Die
            animator.SetTrigger("Die");
        }
    }
}
