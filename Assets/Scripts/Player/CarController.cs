using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    /*
     * Author: Brandon
     * Car control mechanics including driving and shooting
     */

    /*
     * A sphere transform is being moved around instead of the car object, where the car object follows the sphere
     * This allows for the car to move smoothly around the terrain and easier transition between models
     */
    public Rigidbody sphereRigidBody;

    // Car movement statistics
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180f, driftStrength = 300f, driftGravity = 500f, breakForce = 1300f, gravityForce = 10f, dragOnGround = 3f;

    //Values for input
    private float speedInput, turnInput;

    private bool breaking;

    //Boolean to check if car is on ground or not
    private bool grounded;
    //Layer mask for checking what the ground is when grounded
    //Check for grounded is a raypoint on en empty object at the bottom of the car
    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    //Transforms for wheels for turning animations
    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    //Tower transform for aiming gun on the car
    public Transform Tower;
    public float towerSpeed = 300f;
    private float TowerAngleX;

    public ParticleSystem[] driftTrail;
    public float maxEmission = 25;
    private float emissionRate;

    //Bullet prefab the car is currently using
    public Transform bullet;
    public float shootForce;
    //Empty object the bullet will come out of the gun from
    public Transform shootPos;

    void Start()
    {
        /*
         * 
         * Since the car is following the sphere we can't have it move with the car as a child object, but we need it to initially be a child object,
         * So after we get a reference for the object we remove it from its parent and set it alone in the hierarchy
         * 
         */
        sphereRigidBody.transform.parent = null;
    }


    void Update()
    {
        /*
         * Functions for handling horizontal
         */
        turnInput = Input.GetAxis("Horizontal");
        breaking = false;
        //if the car is grounded then it can move, a car can't have air control
        if (grounded)
        {
            /*
             * Drifting Mechanics
             */
            emissionRate = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * driftStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
                sphereRigidBody.AddForce(Vector3.down * gravityForce * driftGravity);
                if (sphereRigidBody.velocity != Vector3.zero || Mathf.Abs(turnInput) < 0f)
                    emissionRate = maxEmission;
            }
            else
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

            if (Input.GetKey(KeyCode.Space))
            {
                breaking = true;
                sphereRigidBody.AddForce(Vector3.down * gravityForce * breakForce);
                if(sphereRigidBody.velocity != Vector3.zero)
                    emissionRate = maxEmission;
            }
        }

        /*
         * Functions for handling forward and backwards movement
         */
        speedInput = 0f;
        if (!breaking)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                    speedInput = Input.GetAxis("Vertical") * forwardAccel * 800f;
                else
                    speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
            }
        }

        /*
         * Particles for drifting and breaks
         */

        foreach (ParticleSystem part in driftTrail)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }

        //Functions for handling tire animations
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);

        //This is where the car object follows the sphere object
        transform.position = sphereRigidBody.transform.position;

        RotateTower();

        /*
         * This is where we get the input for shooting the gun
         */

        if (Input.GetMouseButtonDown(0))
        {
            var instanceBullet = Instantiate(bullet, shootPos.transform.position, shootPos.rotation);
            //Forward force, must be offset due to gun placement
            //TODO may present problems with other cars further testing needed
            instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.right * shootForce * 100f);
            //Upward force
            instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.up * 200f);
        }

    }

    private void FixedUpdate()
    {
        /*
         * Raypoint to check if the car is on the ground
         */
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        //Add gravity if car is in the air, allow car to move if grounded
        if (grounded)
        {
            sphereRigidBody.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                sphereRigidBody.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            sphereRigidBody.drag = 0.1f;

            sphereRigidBody.AddForce(Vector3.up * -gravityForce * 100f);
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
