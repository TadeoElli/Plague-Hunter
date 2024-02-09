using UnityEngine;
using Random = UnityEngine.Random;

public class VenomCloud : Venom
{
    public float lifetime;

    void Start()
    {
        Invoke("DestroyThis", lifetime);
        GameManager.GetAudioManager().PlayClipByName("gasRelease");
    }
    void DestroyThis() => Destroy(this.gameObject);

    void OnTriggerStay(Collider other)
    {
        Damageable dmg = other.GetComponent<Damageable>();
        if (dmg != null)
        {
            DealDamage(dmg);
        }
    }
    void DealDamage(Damageable dmg)
    {
        damage = (int)Random.Range(2f,7f);
        ApplyVenom(dmg);
    }
}
