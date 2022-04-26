using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class AudioManager : MonoBehaviour
{
    public AudioSource truckSound;
    public AudioSource weaponSound;
    public AudioSource driftSound;
    // Start is called before the first frame update
    void Start()
    {
        truckSound = gameObject.AddComponent<AudioSource>();
        weaponSound = gameObject.AddComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
