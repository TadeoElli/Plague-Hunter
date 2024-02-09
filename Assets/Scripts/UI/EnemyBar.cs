using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Damageable damageable;
    public Image HPBar;
    [SerializeField] private float maxHP;
    [SerializeField] private float actHP;
    void Start()
    {
        damageable = GetComponentInParent<Damageable>();
        if (damageable != null)
        {
            maxHP = damageable.life;
            actHP = maxHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damageable != null)
        {
            actHP = damageable.life;
        }
        BarFollow();
    }

    public void BarFollow()
    {
        HPBar.fillAmount = actHP / maxHP;
    }
}
