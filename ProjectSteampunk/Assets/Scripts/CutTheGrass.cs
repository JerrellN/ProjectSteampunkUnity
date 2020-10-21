using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheGrass : MonoBehaviour
{
    public Animator animator;
    public Collider2D theCollider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Is detecting Collision");
        if(other.gameObject.CompareTag("damageBox"))
        {
            animator.SetBool("beenHit", true);
            theCollider.enabled = false;
            StartCoroutine(regrowing());
            Debug.Log("Should be cutting.");
        }
    }

    IEnumerator regrowing()
    {
        yield return new WaitForSeconds(10);
        animator.SetBool("beenHit", false);
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
    }
}
