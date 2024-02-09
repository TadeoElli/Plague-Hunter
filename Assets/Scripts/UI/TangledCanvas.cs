using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangledCanvas : MonoBehaviour
{
    public void SetOn(float maxTime)
    {
        //Debug.Log("SEAT ON");
        gameObject.GetComponentInChildren<TangledBar>().maxTangledTime = maxTime;
        //Debug.Log("SEAT ON");

    }

        public void SetOff()
    {
        gameObject.SetActive(false);
    }
}
