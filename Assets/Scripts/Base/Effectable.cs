using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypes
{
    critic,
    venom,
    fire,
    explosion
}


public class Effectable : MonoBehaviour
{
    public Damageable dmg;
    public Movable move;
    [SerializeField] Effect effects;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movable>();
        dmg = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEffect(Effect effect)
    {
        //this.gameObject.AddComponent<Effect>();

    }
}
