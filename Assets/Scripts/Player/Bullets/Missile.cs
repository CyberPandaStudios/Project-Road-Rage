using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float damage = 20f;

    [SerializeField] private GameObject _particles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
            Instantiate(_particles, transform.position, transform.rotation);
            Destroy(gameObject);
            if(collision.gameObject.tag == "Enemy"){
                collision.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            }
    }

}
