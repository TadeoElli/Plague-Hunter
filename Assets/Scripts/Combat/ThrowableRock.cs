using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.

public class ThrowableRock : Throwable
{
    Rigidbody rig;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float time = 5;
    [SerializeField] float distance;
    public float minDistace, maxDistance;
    public float force;
    private Damageable dmgTarget;
    float basicAttackDamage = 10f;
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
        
    }

    public override void Throw(Vector3 startPos, Vector3 finalPos)
    {
        rig = GetComponent<Rigidbody>();
        transform.position = startPos;
        transform.forward = finalPos - startPos;
        distance = Vector3.Distance(startPos, finalPos);
        Vector3 dirToForce = transform.forward * distance * force;
        Vector3 minDirToForce = transform.forward * minDistace * force;
        Vector3 maxDirToForce = transform.forward * maxDistance * force;
        if(dirToForce.magnitude < minDirToForce.magnitude)
        {
            dirToForce = minDirToForce;
        }
        else if(dirToForce.magnitude > maxDirToForce.magnitude)
        {
            dirToForce = maxDirToForce;
        }
        rig.AddForce(dirToForce + transform.up * 2, ForceMode.Impulse);
        
        Invoke("Destroy", time);
    }
    private void Update() 
    {
        this.transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
        this.transform.Rotate(Vector3.right * 100 * Time.deltaTime);
    }
    void Destroy()
    {
        //Debug.Log("test explosion");
        //GameObject.Instantiate(effectPrefab,this.gameObject.transform);
        Instantiate(effectPrefab, transform.position, new Quaternion());
        Destroy(this.gameObject);
    }
    void DealDamage(Damageable dmg)
    {
        //if (dmg == dmgTarget) //TODO: deberiamos hacer que la plante en realidad le pegue a cualquier bicho?
        //{
        basicAttackDamage = (int)Random.Range(9, 16);
        dmg.TakeDamage(basicAttackDamage, DamageTypes.basic);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        int objLayer = other.gameObject.layer;
        if(objLayer == LayerMask.NameToLayer("Floor") || objLayer == LayerMask.NameToLayer("Objects"))
        {
            GameManager.GetAudioManager().PlayClipByName("RockCrack");
            Destroy();
        }
        else if(other.gameObject.tag == "Player")
        {
            GameManager.GetAudioManager().PlayClipByName("RockCrack");
            Damageable dmg = other.GetComponent<Damageable>();
            if (dmg != null)
                DealDamage(dmg);
            Destroy();
        }
        //Destroy();
        
    }
}
