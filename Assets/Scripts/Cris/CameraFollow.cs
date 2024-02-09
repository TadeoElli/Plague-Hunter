using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public float minX;
    public float maxX;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x + offsetX;

            temp.y = player.position.y + offsetY;

            temp.z = player.position.z + offsetZ;

            transform.position = temp;

        }
    }
}
