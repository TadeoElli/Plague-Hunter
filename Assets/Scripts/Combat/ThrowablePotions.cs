using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablePotions : Throwable
{
    Rigidbody rig;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float time = 5;
    [SerializeField] float distance;
    private float forceMultiplier = 2.5f;

    [SerializeField] bool enableOnWallHit = true;
    LayerMask layerMask = ~0;

    private void Start()
    {
        layerMask = ~0;
        //layerMask += LayerMask.GetMask("Floor");
        //layerMask -= LayerMask.GetMask("Ignore Raycast");
        layerMask |= (1 << LayerMask.NameToLayer("Floor")); //ADD
        layerMask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast")); //REMOVE
        //layerMask += LayerMask.NameToLayer("Floor");
        //layerMask -= LayerMask.NameToLayer("Ignore Raycast");
    }


    public override void Move(Vector3 pos, Vector3 dir)
    {
        rig = GetComponent<Rigidbody>();
        transform.position = pos;
        transform.forward = dir;
        rig.AddForce(transform.forward * distance, ForceMode.VelocityChange);

        Invoke("Destroy", time);
    }

    public override void Throw(Vector3 startPos, Vector3 finalPos)
    {
        rig = GetComponent<Rigidbody>();
        transform.position = startPos;
        transform.forward = finalPos - startPos;
        distance = Vector3.Distance(startPos, finalPos);
        rig.AddForce(transform.forward * distance * forceMultiplier, ForceMode.VelocityChange);
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        //Debug.Log("test explosion");
        //GameObject.Instantiate(effectPrefab,this.gameObject.transform);
        GameManager.GetAudioManager().PlayClipByName("BrakingGlassBottle");
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        int objLayer = other.gameObject.layer;
        if (((1 << objLayer) & layerMask) != 0)
        {
            bool shouldInstantiateOnPosition = !(objLayer != LayerMask.NameToLayer("Floor") && enableOnWallHit == false);
            if (shouldInstantiateOnPosition)
            {
                Instantiate(effectPrefab, transform.position, new Quaternion());
            }
            else
            {
                RaycastHit hitAtk;
                Ray rayAtk = new Ray(this.transform.position, Vector3.down);
                if (Physics.Raycast(rayAtk, out hitAtk, Mathf.Infinity, LayerMask.GetMask("Floor")))
                {
                    Instantiate(effectPrefab, hitAtk.point, new Quaternion());
                }
            }
            Destroy();
        }
    }
}
