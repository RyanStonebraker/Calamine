using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceAnimation : MonoBehaviour {

    Animator animator;

    public GameObject bouncyBall;
    public Vector3 displacementFromFox = new Vector3(5f, 20f, 0f);
    
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("Flick");
            Instantiate(bouncyBall, transform.position + displacementFromFox, new Quaternion());
        }
    }
}
