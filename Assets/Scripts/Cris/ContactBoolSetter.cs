using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactBoolSetter : MonoBehaviour
{
    public Animator animator;
    public string boolName = "IsGrounded";
    public int contactCount;

    private void OnTriggerEnter(Collider other)
    {
        contactCount++;
        if (contactCount > 0)
        {
            animator.SetBool(boolName, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        contactCount--;
        if (contactCount < 1)
        {
            animator.SetBool(boolName, false);
        }
    }
}
