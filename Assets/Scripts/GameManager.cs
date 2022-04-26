using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    CursorLockMode lockMode;
    void Start()
    {
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
