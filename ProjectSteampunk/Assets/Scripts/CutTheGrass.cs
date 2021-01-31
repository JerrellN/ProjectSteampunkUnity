using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheGrass : MonoBehaviour
{
    public Animator animator;
    public Collider2D theCollider;
    public bool isIdle = true;


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
        if (collision.gameObject.CompareTag("damageBox") && isIdle)
        {
            Debug.Log("sword hit grass");
            animator.SetBool("beenHit", true);
            theCollider.enabled = false;
            isIdle = false;
            StartCoroutine(regrowing());
        }
    }

    IEnumerator regrowing()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("beenHit", false);
        regrow();
        yield return new WaitForSeconds(1);
        animator.SetBool("timeToRegrow", false);
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
        isIdle = true;
        
    }
}
