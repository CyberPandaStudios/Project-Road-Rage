using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{
    public Camera main;
    public Camera rear;
    // Start is called before the first frame update
    void Start(){
        main.enabled = true;
        rear.enabled = false;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey("s")){
            main.enabled = false;
            rear.enabled = true;
        }
        else{
            main.enabled = true;
            rear.enabled = false;
        }
    }
}
