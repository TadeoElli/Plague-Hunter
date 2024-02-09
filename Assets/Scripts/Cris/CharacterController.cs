using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //public GameObject key;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public float movementSpeed = 2f;
    float savedSpeed;
    public string jumpButton = "Jump";
    public float jumpForce = 10f;
    public ForceMode jumpForceMode = ForceMode.VelocityChange; // movimiento instantantaneo ignorando masa
    public Rigidbody myRigidbody;
    private Vector3 inputVector;
    public Animator animator;
    public string movementSpeedParameterName = "MovementSpeed";

    public bool keyIconOn;
    public bool canJump;
    public bool canAttack;
    public bool attackingNow;
    public bool canTakeDamage;
    public bool canRoll;
    public bool FoundKey;

    public float attackCooldownTime;
    public float RotationLockCooldown;
    public float jumpCooldownTime;
    public float rollCooldownTime;
    public float InvincibleOnRollTime;

    private void Start()
    {
        canJump = true;
        canAttack = true;
        attackingNow = false;
        canTakeDamage = true;
        canRoll = true;
        FoundKey = false;
        savedSpeed = movementSpeed;
    }

    void Update()
    {
        inputVector.x = Input.GetAxis(horizontalAxis);

        inputVector.z = Input.GetAxis(verticalAxis); // z en unity es profundidad no altura

        if (inputVector.magnitude > 1f) // para normalizar la magnitud diagonal que se pasa de 1 y no valla mas rapido en diagonal
        {
            inputVector.Normalize();
        }

        animator.SetFloat(movementSpeedParameterName, inputVector.magnitude); // le pasamos el valor de la magnitud del imputVector al animator

        if (Input.GetButtonDown(jumpButton) && canJump == true) // Jump
        {
            canJump = false;
            myRigidbody.AddForce(jumpForce * Vector3.up, jumpForceMode);
            Invoke("JumpSetter" , jumpCooldownTime);
        }

        if (Input.GetKeyDown("left shift") && canRoll == true ) // Dodge Roll
        {
            animator.SetTrigger("RollPress");
            canTakeDamage = false;
            canRoll = false;
            canAttack = false;
            Invoke("CanTakeDamageSetter", InvincibleOnRollTime);
            Invoke("CanRollSetter", rollCooldownTime);
            Invoke("CanAttackSetter", rollCooldownTime);
        }

        if (Input.GetMouseButtonDown(0) && canAttack == true) // Ataque con espada
        {
            animator.SetTrigger("AttackPress");
            canAttack = false;
            attackingNow = true;
            movementSpeed = 0f;
            Invoke("CanAttackSetter", attackCooldownTime);
            Invoke("AttackingNowSetter", RotationLockCooldown);
        }
    }

    private void FixedUpdate()
    {
        if (inputVector.sqrMagnitude > 0f && attackingNow == false) // dar la cara siempre en la direccion de movimiento y no poder rotar mientras ataca
        {
            transform.LookAt(transform.position + inputVector);
        }

        myRigidbody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime)); // movimiento con rigidbody
    }

    private void OnCollisionEnter(Collision collision) // contacto con la llave
    {
        if (collision.gameObject.layer == 15) // layer 15 es key
        {
            FoundKey = true;
        }
    }

    void JumpSetter()
    {
        canJump = true;
    }

    void CanTakeDamageSetter()
    {
        canTakeDamage = true;
    }

    void CanRollSetter()
    {
        canRoll = true;
    }

    void CanAttackSetter()
    {
        canAttack = true;
        movementSpeed = savedSpeed;
    }

    void AttackingNowSetter()
    {
        attackingNow = false;
    }
}
