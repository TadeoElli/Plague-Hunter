using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VictoryScene : ChangeScene
{
    void Start()
    {
        Invoke("WaitForEnd", 10);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            WaitForEnd();
        }
    }
    public void WaitForEnd()
    {
        StartCredits();
    }
}
