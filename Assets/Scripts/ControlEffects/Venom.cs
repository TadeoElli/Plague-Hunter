using UnityEngine;

public class Venom : MonoBehaviour
{
    // Start is called before the first frame update

    public float cooldown;
    public float damage;
    private float totalTime = 0;

    protected void ApplyVenom(Damageable dmg)
    {
            totalTime += Time.deltaTime;
            if (totalTime >= cooldown)
            {
                //Damageable dmg = other.GetComponent<Damageable>();
                dmg.TakeDamage(damage, DamageTypes.venom);
                totalTime = 0;
            }
    }
}
