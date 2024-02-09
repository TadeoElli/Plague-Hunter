using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerRotation : MonoBehaviour
{
    Vector3 input;
    Camera mycamera;
    [SerializeField] float rotSpeedQuant;

    private void Start()
    {
        mycamera = FindObjectOfType<Camera>();
    }

    Vector3 last;
    private void Update()
    {
        input = Input.mousePosition;
        Ray ray = mycamera.ScreenPointToRay(input);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point - transform.position;
            point.y = 0;
            transform.forward = Vector3.Slerp(transform.forward, point, rotSpeedQuant * Time.deltaTime);
        }


        Vector3 view = new Vector3(Input.GetAxis("HorizontalView"),0 , Input.GetAxis("VerticalView"));

        if (view.magnitude > 0.5f)
        {
            transform.forward = view;
        }
    }
}
