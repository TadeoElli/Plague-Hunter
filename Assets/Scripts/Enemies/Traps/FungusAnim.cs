using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusAnim : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement pj;
    [SerializeField] Animator animator;
    public float distanceFromPlayer;
    [SerializeField]float distance;

    private void Awake()
    {
        pj = FindObjectOfType<PlayerMovement>();
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, pj.transform.position);
        if (distanceFromPlayer <= distance)
        {
            animator.SetTrigger("Shrink");
        }
    }
}
