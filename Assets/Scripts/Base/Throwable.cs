using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throwable : MonoBehaviour
{
    public abstract void Move(Vector3 pos, Vector3 dir);
    public abstract void Throw(Vector3 startPos, Vector3 finalPos);
}
