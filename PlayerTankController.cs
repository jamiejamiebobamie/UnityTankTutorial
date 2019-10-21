using UnityEngine;
using System.Collections;
public class PlayerTankController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Nozzle;
    private Transform Turret;
    private Transform bulletSpawnPoint;

    private float currentSpeed, targetSpeed, rotationSpeed;
    private float turretRotationSpeed = 150.0f;
    private float maxForwardSpeed = 30.0f;
    private float maxBackwardSpeed = -30.0f;

    //Bullet shooting rate
    protected float shootRate = 0.5f;
    protected float elapsedTime;

    void Start()
    {
        //Tank Settings
        rotationSpeed = 75.0f;

        //Get the turret of the tank
        Turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = Turret.transform;

        elapsedTime = 0.0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateWeapon();
        UpdateControl();
        //Debug.Log(gameObject.transform.position);
    }

    void UpdateWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(elapsedTime);
            if (elapsedTime >= shootRate)
            {
                //Reset the time
                elapsedTime = 0.0f;
                //Debug.Log("hi");

                //Instantiate the bullet
                Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    void UpdateControl()
    {
        //AIMING WITH THE MOUSE
        //Generate a plane that intersects the transform's
        //position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up,
        transform.position + new Vector3(0, 0, 0));
        // How would I color this plane with a material so that I can see it?

        // Generate a ray from the cursor position
        Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Determine the point where the cursor ray intersects
        //the plane.
        float HitDist = 0;

        // If the ray is parallel to the plane, Raycast will
        //return false.
        if (playerPlane.Raycast(RayCast, out HitDist))
        {
            //Get the point along the ray that hits the
            //calculated distance.
            Vector3 RayHitPoint = RayCast.GetPoint(HitDist);
            Quaternion targetRotation = Quaternion.LookRotation(RayHitPoint - transform.position);
            Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, targetRotation, Time.deltaTime * turretRotationSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed = maxForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetSpeed = maxBackwardSpeed;
        }
        else
        {
            targetSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0.0f);
        }

        //Determine current speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 7.0f * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
    }
}