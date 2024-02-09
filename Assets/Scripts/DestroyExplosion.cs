using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
   [SerializeField] float time = 2;

    void Start()
    {
        Invoke("Destroy", time);

    }


    void Destroy()
    {
        Destroy(this.gameObject);
    }


}
