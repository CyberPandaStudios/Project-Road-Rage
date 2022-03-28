using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHolder : MonoBehaviour
{

    [SerializeField] private float speed; 

        private void Update()
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * speed, transform.eulerAngles.z);
        }

}
