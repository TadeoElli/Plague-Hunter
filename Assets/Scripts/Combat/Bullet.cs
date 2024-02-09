using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : Throwable
{
    [SerializeField][Range(5f, 20f)] float speed = 10;

    [SerializeField][Range(0.5f, 2f)] float time = 1f;
    float timer;
    Vector3 pos;

    private Damageable dmgTarget;
    float basicAttackDamage = 5f;
    public GameObject parent;
    //LayerMask layerMask = ~0;

    private void Start()
    {
        //layerMask |= (1 << LayerMask.NameToLayer("Floor")); //ADD
        //layerMask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast")); //REMOVE

        PlayerController player = GameManager.GetPlayer();
        dmgTarget = player.GetComponent<Damageable>();
        pos.y = 0.5f;
        this.transform.position += pos;
    }

    private void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        timer = timer + 1 * Time.deltaTime;
        if (timer > time)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Throw(Vector3 startPos, Vector3 finalPos) { }

    public override void Move(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        transform.forward = dir;
    }

    void DealDamage(Damageable dmg)
    {
        //if (dmg == dmgTarget) //TODO: deberiamos hacer que la plante en realidad le pegue a cualquier bicho?
        //{
        basicAttackDamage = (int)Random.Range(4f, 9f);
        dmg.TakeDamage(basicAttackDamage, DamageTypes.ice);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != parent.gameObject)
        {
            Damageable dmg = other.GetComponent<Damageable>();
            if (dmg != null)
                DealDamage(dmg);

            bool isNotOnRaycastLayer = other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast");
            if (isNotOnRaycastLayer)
                Destroy(this.gameObject);
        }
    }

}