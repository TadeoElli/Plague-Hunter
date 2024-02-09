using UnityEngine;
//TPFinal - Tadeo Elli.

public enum EquipableTypes
{
    potion,
    sword
}
public class PlayerView : MonoBehaviour
{

    private Renderer myRenderer;
    private Material originalMaterial;

    //private GameObject rightHand; //TODO: cambiar lo que tiene en la mano
    //private GameObject leftHand;

    [SerializeField] Animator myAnimator;
    [SerializeField] Material stealthMaterial;
    [SerializeField] GameObject stealthParticle;

    void Start()
    {
        myRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = myRenderer.material;
        //rightHand = GameObject.FindGameObjectWithTag(Tags.RightHand);
        //leftHand = GameObject.FindGameObjectWithTag(Tags.LeftHand);
    }
    public void Attack()
    {
        myAnimator.SetBool("Attack", true);
    }
    public void HeavyAttack()
    {
        myAnimator.SetBool("HeavyAttack", true);
    }

    public void Dodge()
    {
        myAnimator.SetTrigger("Dodge");
        //my_anim.SetBool("Dodge", true);
    }
    public void Drink()
    {
        myAnimator.SetBool("IsHealing", true);
    }
    public void ThrowPotion()
    {
        myAnimator.SetTrigger("Throw");
        myAnimator.SetBool("IsThrowing", true);
        Equip(EquipableTypes.potion);
    }
    public void CancelThrow()
    {
        myAnimator.SetBool("IsThrowing", false);
    }
    public void StopHealing()
    {
        myAnimator.SetBool("IsHealing", false);
    }
    // Update is called once per frame
    public void SetAnimationDir(Vector3 viewDir)
    {
        myAnimator.SetFloat("Horizontal", viewDir.x);
        myAnimator.SetFloat("Vertical" + "", viewDir.z);
    }

    public void Equip(EquipableTypes type)
    {
        //Debug.Log("Equiped: " + type.ToString());
    }

    public void PauseAnimation()
    {
        myAnimator.enabled = false;
    }
    public void StartAnimation()
    {
        myAnimator.enabled = true;
    }

    // esto esta igual que el default de Damagable
    public void ReceiveDamage(Damageable dmg)
    {
        myRenderer.material = Resources.Load<Material>("Art/Materials/red");
        Invoke("FinishReceiveDamage", 0.3f);
        
    }
    public void Stealth()
    {
        myRenderer.material = stealthMaterial;
        stealthParticle.SetActive(true);
        //Invoke("FinishReceiveDamage", 0.3f);
    }

    public void FinishReceiveDamage()
    {
        myRenderer.material = originalMaterial;
    }
    public void FinishStealth()
    {
        myRenderer.material = originalMaterial;
        stealthParticle.SetActive(false);
    }
    public void Death()
    {
        myAnimator.SetTrigger("Death");
    }
}
