using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    // Start is called before the first frame update
    Damageable dmg;

    [SerializeField] Animator my_anim;
    
    void Start()
    {
        dmg = GetComponent<Damageable>();
        dmg.setOnTakeDamageCallback(TakeDamageCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamageCallback(Damageable dmg)
    {
        my_anim.SetTrigger("Shake");
    }
}
