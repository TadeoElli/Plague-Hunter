using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    Vector3 input;
    Camera mycamera;
    [SerializeField] float rotSpeedQuant;

    public bool enableLookAtMouse = true;

    private void Start()
    {
        mycamera = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        if (enableLookAtMouse)
        {
            input = Input.mousePosition;
            Ray ray = mycamera.ScreenPointToRay(input);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.point - transform.position;
                point.y = 0;
                SetLookAt(Vector3.Slerp(LookingAt(), point, rotSpeedQuant * Time.deltaTime));
            }
        }
    }
    public Vector3 LookingAt()
    {
        return transform.forward;
    }
    public void SetLookAt(Vector3 dir)
    {
        transform.forward = dir;
    }
}
