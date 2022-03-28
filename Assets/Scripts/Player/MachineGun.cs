using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    //Tower transform for aiming gun on the car
    public Transform Tower;
    public float towerSpeed = 300f;
    private float TowerAngleX;

    //Bullet prefab the car is currently using
    public Transform bullet;
    public float shootForce;
    //Empty object the bullet will come out of the gun from
    public Transform shootPos;


    /*

        0 = single fire
        1 = rapid fire

    */
    public int fireType = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTower();

        /*
         * This is where we get the input for shooting the gun
         */
        if(fireType == 0){
            if (Input.GetMouseButtonDown(0)){
                var instanceBullet = Instantiate(bullet, shootPos.transform.position, Tower.rotation);
                instanceBullet.transform.Rotate(0, 180, 0);
                //Forward force, must be offset due to gun placement
                //TODO may present problems with other cars further testing needed
                instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.right * shootForce * 100f);
                //Upward force
                instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.up * 500f);
            }
        }else if (fireType == 1){
            if (Input.GetMouseButton(0)){
                var instanceBullet = Instantiate(bullet, shootPos.transform.position, Tower.rotation);
                instanceBullet.transform.Rotate(0, 180, 0);
                //Forward force, must be offset due to gun placement
                //TODO may present problems with other cars further testing needed
                instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.right * shootForce * 100f);
                //Upward force
                instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.up * 500f);
            }
        }

    }

    /*
     * Gun rotation function based on mouse x axis 
     */
    private void RotateTower()
    {
        TowerAngleX += Input.GetAxis("Mouse X") * towerSpeed * -Time.deltaTime;
        TowerAngleX = Mathf.Clamp(TowerAngleX, -60, 120);
        Tower.localRotation = Quaternion.Euler(TowerAngleX, 90, 90);
    }

}
