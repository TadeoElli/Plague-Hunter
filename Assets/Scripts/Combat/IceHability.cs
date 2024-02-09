using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHability : OilEffect
{
    PlayerCombat combat;
    [SerializeField] GameObject ice;
    [SerializeField] float time;
    [SerializeField] float timer;
    //bool active = false;

    /*
    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
    }
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        IcePotion();

        if (active == true)
        {
            timer = timer + 1 * Time.deltaTime;

        }
    }


    void IcePotion()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.GetAudioManager().PlayClipByName("TrapFreeze");
            ice.SetActive(true);
            active = true;
            combat.weapon_on_ice = true;

        }
        else if (timer >= time)
        {
            ice.SetActive(false);
            active = false;
            combat.weapon_on_ice = false;
            timer = 0;
        }
    }
    */
    public override void StartEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EndEffect()
    {
        throw new System.NotImplementedException();
    }

}
