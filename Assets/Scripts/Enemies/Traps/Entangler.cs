using UnityEngine;

public class Entangler : AutodestroyedTrap
{
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            //Debug.Log($"{other.gameObject.name} enter ice");
            EntityOnFrozen onFrozen = other.gameObject.GetComponentInChildren<EntityOnFrozen>();
            onFrozen.ApplyEffect();
            //Debug.Log("Shackalaka");
        }
        base.OnTriggerEnter(other);
    }
}
