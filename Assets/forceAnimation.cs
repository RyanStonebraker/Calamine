using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceAnimation : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(!animator.GetBool("SitStay"))
                animator.SetTrigger("Sit");

            animator.SetBool("SitStay", !animator.GetBool("SitStay"));
            Debug.Log("F registed down");
        }
    }
}
