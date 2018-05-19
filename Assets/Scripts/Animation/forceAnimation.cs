using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceAnimation : MonoBehaviour {

    Animator animator;

    public GameObject bouncyBall;
    public GameObject countedLoop;
    public GameObject loopCounterObject;
    public GameObject loopBodyObject;
    public Vector3 displacementFromFox = new Vector3(5f, 20f, 0f);
    public Vector3 displacementFromSteamVR = new Vector3(1.17f, -3.8f, -14.5f);

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
	}
	
    private void spawnCountLoop()
    {
        Instantiate(countedLoop, GameObject.Find("SteamVR").transform.position + displacementFromSteamVR, new Quaternion());
    }

    private void displayLabel(GameObject label, Vector3 position, Quaternion rotation)
    {
        animator.SetTrigger("Point");
        GameObject arrowWithLabel = Instantiate(label, position, rotation);
        Component[] animatorsInChildren = arrowWithLabel.GetComponentsInChildren<Animator>();

        foreach (Animator animator in animatorsInChildren) // emphasize object with scale up animation
        {
            if (animator.name == "Label")
            {
                Debug.Log("Label found in " + animator);
                animator.SetTrigger("Grow");
            }
        }
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!animator.GetBool("SitStay"))
            {
                animator.SetTrigger("CountedLoop");
                Invoke("spawnCountLoop", 2.5f);
            }

            animator.SetBool("SitStay", !animator.GetBool("SitStay"));
            Debug.Log("F registed down");
        }

        if (Input.GetKeyDown(KeyCode.R))
            displayLabel(loopCounterObject,
                         GameObject.Find("CountedLoop(Clone)").transform.position + 
                         new Vector3(-4.37991301f, 1.85409f, 8.6065f),
                         Quaternion.Euler(0f, 32.307f, 0f));

        if (Input.GetKeyDown(KeyCode.T))
            displayLabel(loopBodyObject,
                         GameObject.Find("CountedLoop(Clone)").transform.position +
                         new Vector3(-0.44791301f, 0.94609f, 7.1945f),
                         Quaternion.Euler(0f, 0f, 0f));
    }
}
