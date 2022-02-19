using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public Rigidbody sphereRigidBody;

    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180, gravityForce = 10f, dragOnGround = 3f;

    private float speedInput, turnInput;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    public Transform Tower;
    public float towerSpeed = 300f;
    private float TowerAngleX;

    public Transform bullet;
    public float shootForce;
    public Transform shootPos;
    private bool shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        sphereRigidBody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
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

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);

        transform.position = sphereRigidBody.transform.position;

        RotateTower();

        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;

        } else if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
        }

        if (shooting)
        {
            var instanceBullet = Instantiate(bullet, shootPos.transform.position, shootPos.rotation);
            instanceBullet.GetComponent<Rigidbody>().AddForce(Tower.right * shootForce * 100f);
        }

    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
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

    private void RotateTower()
    {
        TowerAngleX += Input.GetAxis("Mouse X") * towerSpeed * -Time.deltaTime;
        TowerAngleX = Mathf.Clamp(TowerAngleX, -60, 120);
        Tower.localRotation = Quaternion.Euler(TowerAngleX, 90, 90);
    }



}
