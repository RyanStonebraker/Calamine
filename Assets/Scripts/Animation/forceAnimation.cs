using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceAnimation : MonoBehaviour {

    private Animator animator;
    private uint sceneNumber;
    private TransitionText tayAISpeechBubble;
    private GameObject nonInteractableCountLoop;

    public GameObject bouncyBall;
    public GameObject countedLoopWithLabel;
    public GameObject loopCounterObject;
    public GameObject loopBodyObject;
    public GameObject makeBallBlock;
    public GameObject interactableCountLoop;
    public GameObject startBlock;
    public Vector3 displacementFromFox = new Vector3(5f, 20f, 0f);
    public Vector3 displacementFromSteamVR = new Vector3(1.17f, -3.8f, -14.5f);
    public UnityEngine.UI.Text TayAIUIText;


    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        sceneNumber = 0;
        tayAISpeechBubble = TayAIUIText.GetComponent<TransitionText>();
	}
	
    private void spawnCountLoop()
    {
        nonInteractableCountLoop = Instantiate(countedLoopWithLabel, GameObject.Find("SteamVR").transform.position + displacementFromSteamVR, new Quaternion());
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

    private void sitAndSpawnCountLoop()
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
                         nonInteractableCountLoop.transform.position +
                         new Vector3(-4.37991301f, 1.85409f, 8.6065f),
                         Quaternion.Euler(0f, 32.307f, 0f));
    }

    private void spawnLoopBodyLabel()
    {
        displayLabel(loopBodyObject,
                         nonInteractableCountLoop.transform.position +
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

    // TODO (Tristan): In and ambient animation
    private void spawnInteractableCountLoop() 
    {
        GameObject countLoop = Instantiate(interactableCountLoop,
                                   GameObject.Find("SteamVR").transform.position +
                                   new Vector3(1.528087f, -2.27291f, -1.7185f),
                                   new Quaternion());
    }

    // TODO (Tristan): In and ambient animation
    private void spawnStartBlock()
    {
        GameObject spawnedStartBlock = Instantiate(startBlock,
                                   GameObject.Find("SteamVR").transform.position +
                                   new Vector3(1.528087f, -2.27291f, -1.7185f),
                                   new Quaternion());
    }

    private void animateNextScene()
    {
        sceneNumber++;
        switch (sceneNumber)
        {
            case 1:
                tayAISpeechBubble.changeTextWithEffect("You can use loops to make part of your program happen more than once.");
                break;
    
            case 2:
                tayAISpeechBubble.changeTextWithEffect("There are two types of loops in Calamine.");
                break;

            case 3:
                tayAISpeechBubble.changeTextWithEffect("The Counted Loop and the Check Loop");
                Invoke("sitAndSpawnCountLoop", 1f);
                break;

            case 4:
                tayAISpeechBubble.changeTextWithEffect("In this lesson, we’ll only be talking about the Counted Loop.");
                break;

            case 5:
                tayAISpeechBubble.changeTextWithEffect("Let’s say we have a program that makes a ball bounce in the run area.");
                Invoke("flickTailSpawnBall", 1.5f);
                /**************************************************************/
                /* ADD MAKE BALL w/ RED COLOR ARGUMENT ON DRAFTING BOARD HERE */
                /**************************************************************/
                break;

            case 6:
                tayAISpeechBubble.changeTextWithEffect("But we want a program that makes twenty balls bouncing around the run area.");
                break;

            case 7:
                tayAISpeechBubble.changeTextWithEffect("We could put twenty of these \"Make Ball\" blocks on the drafting board,");
                break;

            case 8:
                tayAISpeechBubble.changeTextWithEffect("but that would take a long time and make our program look messy.");
                break;

            case 9:
                tayAISpeechBubble.changeTextWithEffect("Luckily, we can do this really easily with a loop!");
                Invoke("spawnInteractableCountLoop", 1.2f);
                tayAISpeechBubble.changeTextWithEffect("Go ahead and put this Counting Loop onto the drafting board.");
                break;

            case 10:
                tayAISpeechBubble.changeTextWithEffect("Now that we have our loop on the board, we need to tell the loop how many times it should repeat.");
                break;

            case 11:
                tayAISpeechBubble.changeTextWithEffect("Increase that 1 on the loop to 20; this is called the loop counter.");
                Invoke("spawnLoopCounterLabel", 1f);
                break;

            case 12:
                tayAISpeechBubble.changeTextWithEffect("Good!");
                break;

            case 13:
                tayAISpeechBubble.changeTextWithEffect("Right now our loop is empty.");
                break;

            case 14:
                tayAISpeechBubble.changeTextWithEffect("We need to tell the loop what it needs to do twenty times.");
                break;

            case 15:
                tayAISpeechBubble.changeTextWithEffect("Put this \"Make Ball\" block into the empty slot on the loop.");
                Invoke("spawnMakeBallBlock", 1f);
                break;

            case 16:
                tayAISpeechBubble.changeTextWithEffect("This is called the \"Loop Body.\"");
                Invoke("spawnLoopBodyLabel", 1f);
                break;

            case 17:
                tayAISpeechBubble.changeTextWithEffect("Fantastic! Now all we need to do is run the program!");
                Invoke("spawnStartBlock", 1f);
                /* Run button appears and 20 balls bounce in run scene */
                break;

            case 18:
                tayAISpeechBubble.changeTextWithEffect("Now write a program on your own that makes 2 foxes in the run area.");
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
            sitAndSpawnCountLoop();

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
