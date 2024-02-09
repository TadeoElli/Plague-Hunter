using System.Numerics;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
//TPFinal - Tadeo Elli.
public enum DamageTypes
{
    basic,
    venom,
    fire,
    ice,
    explosion,
    critical,
    player
}
public class Damageable : MonoBehaviour
{
    public List<DamageTypes> EffectInvulnerabilty;
    [SerializeField] public GameObject numberUI;
    [SerializeField] String animatorTakeDamageTriggerName;
    [SerializeField] Animator animator;

    [SerializeField] public float life;
    [SerializeField] public float maxLife;
    Action<Damageable> deathCallback;
    Action<Damageable> onTakeDamageCallback;
    private Renderer myRenderer;
    private Material originalMaterial;
    //[SerializeField] string soundToPlay;

    private EntityOnFire onFire;

    void Start()
    {
        if (EffectInvulnerabilty == null)
        {
            EffectInvulnerabilty = new List<DamageTypes>();
        }
        if (onTakeDamageCallback == null)
        {
            this.setOnTakeDamageCallback(ReceiveDamage);
            NotImplemented();
        }
        if (deathCallback == null)
        {
            this.setDeathCallback(DefaultDeath);
            NotImplemented();
        }
        myRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = myRenderer.material;

        onFire = GetComponentInChildren<EntityOnFire>();
        //Debug.Log(onFire);
        if (onFire != null)
        {
            //Debug.Log($"Siiiiiiiiuuuu {this.gameObject.name}");
            onFire.SetDamagable(this);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Death();
        }
    }

    void NotImplemented()
    {
        //Debug.Log("functions in dmg not implemented");
        //throw new NotImplementedException();
    }




    public void TakeDamage(float dmg, DamageTypes type)
    {
        if(this.gameObject != null)
        {
            if (!EffectInvulnerabilty.Contains(type))
            {

                Debug.Log($"{gameObject.name} Received {dmg} damage of type {type}");
                life = life - dmg;

                var invokeNumber = Instantiate(numberUI, transform.position + 0.5f * Random.onUnitSphere, transform.rotation);
                if (this.gameObject.CompareTag(Tags.Player))
                {
                    type = DamageTypes.player;
                }
                invokeNumber.GetComponent<DamageNumbers>().Inicializar(dmg, type);
                //var data = new DamageData(this, type, dmg);
                onTakeDamageCallback.Invoke(this);
            }
        }
    }

    void Death()
    {
        if(this.gameObject != null)
        {
            Debug.Log("Rip");
            //GameManager.GetAudioManager().PlayClipByName(soundToPlay);
            ItemDropper itemDrop = this.gameObject.GetComponent<ItemDropper>();
            if (itemDrop != null)
                itemDrop.Drop();
            else
                Debug.Log("Drop not founded");
            deathCallback.Invoke(this);
            Destroy(this);
        }
    }
    public void setDeathCallback(Action<Damageable> callback)
    {
        deathCallback = callback;
    }
    public void setOnTakeDamageCallback(Action<Damageable> callback)
    {
        onTakeDamageCallback = callback;
    }


    //TODO: volar esto de damagable.
    // DEFAULTS DE DAÑO
    void ReceiveDamage(Damageable dmg)
    {
        if(this.gameObject != null)
        {
            if (animator != null && animatorTakeDamageTriggerName != null)
                animator.SetTrigger(animatorTakeDamageTriggerName);
            myRenderer.material = Resources.Load<Material>("Art/Materials/red");// poner particulas de sangre aca
            Invoke("FinishReceiveDamage", 0.3f);
        }
    }

    void FinishReceiveDamage()
    {
        myRenderer.material = originalMaterial;
    }

    void DefaultDeath(Damageable dmg)
    {
        Destroy(dmg.gameObject);
    }

    public void ApplyFireDamage()
    {
        if(this.gameObject != null)
        {
            onFire.ApplyFireDamage();
        }
    } 
    public void SetOnFire()
    {
        if(this.gameObject != null)
        {
            onFire.ApplyEffect();
        }
    }

}

/*public class DamageData
{
    public Damageable dmg;
    public DamageTypes type;
    public float damage;

    DamageData(Damageable _dmg, DamageTypes _type, float _damage)
    {
        dmg = _dmg;
        type = _type;
        damage = _damage;
    }
}
*/
