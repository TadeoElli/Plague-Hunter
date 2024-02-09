using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] Transform target;
    [SerializeField] float speed = 1f;
    [Range(2,10)] public float mindistancetoattack = 5f;
    [SerializeField] Renderer myRender;

    GenericShooter shootpulse;

    [SerializeField] Bullet model;

    [SerializeField] List<Transform> positions;
    float timer;
    [SerializeField] float time_to_close = 5f;
    int index = 0;

    [SerializeField] Transform shootpoint;


    private void Start()
    {
        shootpulse = GetComponent<GenericShooter>();
        target = GameManager.GetPlayer().transform;

        shootpulse.Configure(RealShoot);
    }

    void RealShoot() //cada un segundo se ejeucta esto
    {
        Bullet bullet = GameObject.Instantiate(model);
        bullet.Move(shootpoint.position, shootpoint.forward);
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, target.position) < mindistancetoattack)
        {
            myRender.material.color = Color.red;
            //myRender.material.SetColor("_Color", Color.red);
            shootpulse.Play();

            LookAt();
        }
        else
        {
            myRender.material.color = Color.green;
            pivot.forward = Vector3.Slerp(pivot.forward, Vector3.up, Time.deltaTime * speed);

            shootpulse.Stop();
        }

        if (timer < time_to_close)
        {
            timer = timer + 1 * Time.deltaTime;
            if (index == positions.Count - 1)
            {
                transform.position = Vector3.Lerp(positions[index].position, positions[0].position, timer/time_to_close);
            }
            else
            {
                transform.position = Vector3.Lerp(positions[index].position, positions[index + 1].position, timer / time_to_close);
            }    
        }
        else
        {
            timer = 0;
            NextIndex();
        }
    }
    void NextIndex()
    {
        if (index == positions.Count - 1) //si llega al maximo
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }

    void LookAt()
    {
        Vector3 flechita = target.position - pivot.position;
        flechita.y = 0;
        pivot.forward = Vector3.Slerp(pivot.forward, flechita, Time.deltaTime * speed);
    }
}
