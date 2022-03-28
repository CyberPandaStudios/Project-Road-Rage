using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
     * Author: Brandon
     * 
     * A singleton class that holds the player object in it at all times allowing for other scripts
     * to get the player without having to search through each object in the scene every time.
     */


    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    void Start(){
        
    }

}
