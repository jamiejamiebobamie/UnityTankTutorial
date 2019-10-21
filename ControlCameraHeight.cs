using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCameraHeight : MonoBehaviour
{

    protected GameObject[] enemies;
    private GameObject marker;
    private GameObject player;

    public GameObject terrain;

    private float cameraY;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        //Instantiate the marker
        marker = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), Vector3.zero, Quaternion.identity);
        marker.name = "Marker";
    }

    Vector3 FindEnemyAverageLocation()
    {
        Vector3 averageLocation = Vector3.zero;
        foreach(GameObject enemy in enemies)
        {
            averageLocation += enemy.transform.position;
        }

        return averageLocation / enemies.Length;
    }

    float RaiseAndLowerCamera()
    {
        float height = Vector3.Distance(player.transform.position, marker.transform.position);
        float max = 40.0;
        float min = 20;
        if (height <= min)
        {
            height = min;
        }
        else if (height >= max)
        {
            height = max;
        }

        terrain.transform.localScale.x / max; // trying to make ?? the height
        return height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goHereMarker = FindEnemyAverageLocation();
        marker.transform.position = goHereMarker;

        cameraY = RaiseAndLowerCamera();

 
    }

    private void LateUpdate()
    {
        Debug.Log(cameraY);
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, cameraY / 2, gameObject.transform.position.z);
    }
}
