using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject player;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Instantiate(barrel, spawn.transform.position, barrel.transform.rotation);

        }
    }

}
