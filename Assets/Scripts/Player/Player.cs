using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;
    public Vector3 position;


    public HealthBar healthBar;


    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if(healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        animator = gameObject.GetComponent<Animator>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            //Die

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }
    }
}
