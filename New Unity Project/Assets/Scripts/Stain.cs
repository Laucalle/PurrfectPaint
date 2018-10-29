using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour {

    Animator m_Animator;

    bool invincible = true;
    float waitTilInvincible = 0.5f;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(invincible)
        {
            if (waitTilInvincible <= 0)
            {
                invincible = false;
            }
            waitTilInvincible -= Time.deltaTime;
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stain") && !invincible)
        {
            m_Animator.SetBool("disappear", true);
            StartCoroutine(Disappear());
        }
    }
    
    /*
    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(0.5f);
        invincible = false; 
        
    }
    */
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }




}
