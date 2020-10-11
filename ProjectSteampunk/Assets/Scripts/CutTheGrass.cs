using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheGrass : MonoBehaviour
{
    public Animator animator;
    public Collider2D theCollider;
    public Rigidbody2D grassRb;
    public float regrowTime = 5.0f;
    public float timeElapsed = 0;
    private bool beenHit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("damageBox"))
        {
            beenHit = true;
            animator.SetBool("beenHit", beenHit);
            theCollider.enabled = false;
            StartCoroutine(regrowing());
        }
    }

    IEnumerator regrowing()
    {
        yield return new WaitForSeconds(regrowTime);
        beenHit = false;
        animator.SetBool("beenHit", beenHit);
        regrow();
    }

    private void regrow()
    {
        animator.SetBool("timeToRegrow", true);
        theCollider.enabled = true;
        returnToIdle();
    }

    private void returnToIdle()
    {
        animator.SetBool("idle", true);
        timeElapsed = 0;
    }
}
