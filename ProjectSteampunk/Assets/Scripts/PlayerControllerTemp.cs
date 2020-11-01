using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTemp : MonoBehaviour
{
    [Header("Base Setting")]
    public int playerId;


    [Space]
    [Header("Attributes")]
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool attacking;
    public GameObject crossHair;
    public float crossHair_Distance = 1.0f;
    public bool endOfAiming;
    public bool isAttacking;
    public bool attack;
    public float AttackPenalty = 0f;
    public float attackTimer = 0.0f;
    public float attackingTime = 1.0f;
    public int hitNo = 0;

    [Space]
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D theCollider;

    [Space]
    [Header("Prefabs")]


    [Space]
    [Header("Character Attributes")]
    public float movementBaseSpeed = 1.0f;
    public float health = 100.0f;
    public float ATK = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Move();
        Animate();
        Aim();
        Attack();
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("FirstSwing"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    void ProcessInput()
    {
        

        endOfAiming = Input.GetButton("Fire1");
        attack = Input.GetButton("Fire1");

        if (isAttacking)
        {
            movementSpeed *= AttackPenalty;
            attackTimer = attackingTime;
        }
        else
        {
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementDirection.Normalize();
        }
        if (attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;
        }

    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        animator.SetFloat("Speed", movementSpeed);

        if (attack)
        {
            StartCoroutine(Combo());
            if (hitNo == 1)
            {
                animator.SetBool("IsAttacking", true);
                animator.SetInteger("attackNo", 1);
            }
            else if (hitNo == 2)
            {
                animator.SetBool("IsAttacking", true);
                animator.SetInteger("attackNo", 2);
            }
            else if (hitNo > 2)
            {
                hitNo = 0;
                animator.SetBool("IsAttacking", true);
                animator.SetInteger("attackNo", 1);
            }
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsAttacking", false);
        }
    }

    public IEnumerator Combo()
    {
        hitNo += 1;

        yield return new WaitForSeconds(0.5f);

        hitNo = 0;
    }


    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            crossHair.transform.localPosition = movementDirection * crossHair_Distance;

        }
    }

    void Attack()
    {
        Vector2 attackDirection = crossHair.transform.localPosition;
        attackDirection.Normalize();
    }
}

