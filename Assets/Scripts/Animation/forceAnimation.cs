using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceAnimation : MonoBehaviour {

    private Animator animator;
    private uint sceneNumber;

    public GameObject bouncyBall;
    public GameObject countedLoop;
    public GameObject loopCounterObject;
    public GameObject loopBodyObject;
    public GameObject makeBallBlock;
    public Vector3 displacementFromFox = new Vector3(5f, 20f, 0f);
    public Vector3 displacementFromSteamVR = new Vector3(1.17f, -3.8f, -14.5f);



    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        sceneNumber = 0;
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

    private void sit()
    {
        if (!animator.GetBool("SitStay"))
            animator.SetTrigger("Sit");

        animator.SetBool("SitStay", !animator.GetBool("SitStay"));
    }

    private void flickTailSpawnBall()
    {
        animator.SetTrigger("Flick");
        Instantiate(bouncyBall, transform.position + displacementFromFox, new Quaternion());
    }

    private void sitSpawnCountLoop()
    {
        if (!animator.GetBool("SitStay"))
        {
            animator.SetTrigger("CountedLoop");
            Invoke("spawnCountLoop", 2.5f);
        }

        animator.SetBool("SitStay", !animator.GetBool("SitStay"));
    }

    private void spawnLoopCounterLabel()
    {
        displayLabel(loopCounterObject,
                         GameObject.Find("CountedLoop(Clone)").transform.position +
                         new Vector3(-4.37991301f, 1.85409f, 8.6065f),
                         Quaternion.Euler(0f, 32.307f, 0f));
    }

    private void spawnLoopBodyLabel()
    {
        displayLabel(loopBodyObject,
                         GameObject.Find("CountedLoop(Clone)").transform.position +
                         new Vector3(-0.44791301f, 0.94609f, 7.1945f),
                         Quaternion.Euler(0f, 0f, 0f));
    }

    private void spawnMakeBallBlock()
    {
        GameObject ballBlock = Instantiate(makeBallBlock,
                                   GameObject.Find("SteamVR").transform.position +
                                   new Vector3(1.528087f, -2.27291f, -1.7185f),
                                   new Quaternion());

        ballBlock.GetComponent<Animator>().SetTrigger("SpinIn");
        // The block should be kinematic by some nature until the player grabs the block
        // at which point it should switch to use gravity and interact like a normal object.
        // this should be taken care of inside of the spawn ball block script.
    }

    private void animateNextScene()
    {
        switch(sceneNumber)
        {
            case 1:

            break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F))
            sit();

        if (Input.GetKeyDown(KeyCode.G))
            flickTailSpawnBall();

        if (Input.GetKeyDown(KeyCode.V))
            sitSpawnCountLoop();

        if (Input.GetKeyDown(KeyCode.R))
            spawnLoopCounterLabel();

        if (Input.GetKeyDown(KeyCode.T))
            spawnLoopBodyLabel();

        if (Input.GetKeyDown(KeyCode.Y))
            spawnMakeBallBlock();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            animateNextScene();
            
    }
}
