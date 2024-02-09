using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private Damageable dmg;
    private PlayerController player;
    private float initialLife;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        player = GameManager.GetPlayer();
        dmg = player.GetComponent<Damageable>();
        initialLife = dmg.maxLife;
        slider.maxValue = initialLife;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = dmg.life;
    }
}
