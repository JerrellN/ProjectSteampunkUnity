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
    public GameObject crossHair;
    public float crossHair_Distance = 1.0f;
    public bool attackKey;
    public bool isAttacking;
    public float aimingBasePenalty = 0.0f;
    public float attackTimer = 0.0f;
    public float attackingTime = 1.0f;
    public int attackNum = 0;

    [Space]
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D collider;


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
        if(attackNum > 2)
        {
            attackNum = 0;
        }
    }

    void ProcessInput()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        //endOfAiming = Input.GetButton("Fire1");
        attackKey = Input.GetButton("Fire1");

        if (attackKey)
        {
            Attack();
        }
        //if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    isAttacking = false;
        //    Debug.Log("The player is not attacking");
        //}

    }

    private void Attack()
    {
        this.animator.SetBool("IsAttacking", true);
        if(this.animator.GetBool("IsAttacking") == true)
        {
            Debug.Log("The variable IsAttacking in the animator was set to true.");
        }
        animator.SetInteger("attackNo", attackNum + 1);
        //isAttacking = true;
        movementSpeed *= aimingBasePenalty;
        //animator.SetBool("IsAttacking", false);
        //isAttacking = false;
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

        if (isAttacking)
        {
            animator.SetBool("IsAttacking", true);
            
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            crossHair.transform.localPosition = movementDirection * crossHair_Distance;

        }
    }

    //void Shoot()
    //{
    //    Vector2 shootDirection = crossHair.transform.localPosition;
    //    shootDirection.Normalize();

    //    if (endOfAiming)
    //    {
    //        GameObject arrow = Instantiate(attackPrefab, transform.position, Quaternion.identity);
    //    }
    //}
}

