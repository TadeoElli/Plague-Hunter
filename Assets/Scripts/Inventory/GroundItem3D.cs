using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundItem3D : GroundItem
{
    //public bool isPickable = false;
    Rigidbody rig;
    [SerializeField] bool freeze = false;

    Collider[] colliders;

    private void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        colliders = this.GetComponents<Collider>();
        //foreach (Collider col in colliders)
        //    col.enabled = false;
        Invoke("Reenable", 0.5f);
        if(!freeze)
        {
            Vector3 dir = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)).normalized;
            rig.AddForce(dir * 5, ForceMode.VelocityChange);
        }
    }
    void Reenable()
    {
        //isPickable = true;
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
}
