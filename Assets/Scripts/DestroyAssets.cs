using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAssets : MonoBehaviour
{
    [SerializeField] float time;
    void Start()
    {
        Invoke("Destroy", time);

    }



    void Destroy()
    {
        Destroy(this.gameObject);
    }

}
