using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health = 200f;
    public Vector3 position;
    
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
              animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage){
        health -= damage;
        if(health <= 0){
            //Die
            animator.SetTrigger("Die");
        }
    }
}
