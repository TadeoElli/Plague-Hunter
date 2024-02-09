
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionPreview : MonoBehaviour
{
    SpriteRenderer my_render;
    [SerializeField] private Color newColor = Color.white;
    [SerializeField] private Color oldColor = Color.white;
    [SerializeField] private float maxDistance = 10.5f;
    float distance;
    [SerializeField] LayerMask hitMask;
    private Transform target;

    void Start()
    {
        my_render = GetComponent<SpriteRenderer>();
        PlayerController player = GameManager.GetPlayer();
        target = player.transform;
    }

    void Update()
    {
        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayAtk, out hitAtk, Mathf.Infinity,hitMask))
        {
            this.transform.position = hitAtk.point + new Vector3(0, 0.02f, 0);
        }

        distance = Vector3.Distance(this.transform.position, target.position);
        if (distance > maxDistance)
        {
            my_render.color = newColor;
        }
        else
        {
            my_render.color = oldColor;
        }
    }
    public bool IsInRange()
    {
        if (distance > maxDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ActivatePotionPreview()
    {
        my_render.enabled = true;
    }

    public void DesactivatePotionPreview()
    {
        my_render.enabled = false;
    }

    public void ShowPotionFeedback()
    {

        RaycastHit hitAtk;
        Ray rayAtk = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayAtk, out hitAtk, Mathf.Infinity, hitMask))
        {
            Vector3 pos = hitAtk.point + new Vector3(0, 0.02f, 0);
            //pos.y = 0.2f;
            this.transform.position = pos;
        }
    }
}