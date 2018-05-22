using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class forceAnimation : MonoBehaviour {

    private Animator animator;
    private uint sceneNumber;
    private TransitionText tayAISpeechBubble;
    private GameObject nonInteractableCountLoop;
    private GameObject steamVR;

    public GameObject bouncyBall;
    public GameObject countedLoopWithLabel;
    public GameObject loopCounterObject;
    public GameObject loopBodyObject;
    public GameObject makeBallBlock;
    public GameObject interactableCountLoop;
    public GameObject startBlock;
    public Vector3 displacementFromSteamVR = new Vector3(1.17f, -3.8f, -14.5f);

    public GameObject playArea;
    public UnityEngine.UI.Text TayAIUIText;


    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        sceneNumber = 0;
        tayAISpeechBubble = TayAIUIText.GetComponent<TransitionText>();
        steamVR = GameObject.Find("SteamVR");
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
        Vector3 playAreaCenter = playArea.transform.position;
        GameObject spawnedBall = Instantiate(bouncyBall, new Vector3(playAreaCenter.x, playAreaCenter.y + 20f, playAreaCenter.z), new Quaternion());
        Destroy(spawnedBall, 10f);
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
                                   steamVR.transform.position +
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
                                   steamVR.transform.position +
                                   new Vector3(1.528087f, -2.27291f, -1.7185f),
                                   new Quaternion());

        countLoop.GetComponent<Animator>().SetTrigger("FlipIn");
    }

    // TODO (Tristan): In and ambient animation
    private void spawnStartBlock()
    {
        GameObject spawnedStartBlock = Instantiate(startBlock,
                                   steamVR.transform.position +
                                   new Vector3(1.528087f, -2.27291f, -1.7185f),
                                   new Quaternion());

        spawnedStartBlock.GetComponent<Animator>().SetTrigger("SpawnInTwitch");
    }

    private void playVoiceRecording(string recExtension) {
        gameObject.GetComponent<AudioSource>().clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/calamine_voiceOver " + recExtension + ".wav", typeof(AudioClip));
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void animateNextScene()
    {
        sceneNumber++;
        switch (sceneNumber)
        {
            case 1:
                tayAISpeechBubble.changeTextWithEffect("You can use loops to make part of your program happen more than once.");
                playVoiceRecording("02");
                break;
            case 2:
                tayAISpeechBubble.changeTextWithEffect("There are two types of loops in Calamine.");
                playVoiceRecording("03");
                break;

            case 3:
                tayAISpeechBubble.changeTextWithEffect("The Counted Loop and the Check Loop");
                playVoiceRecording("04");
                Invoke("sitAndSpawnCountLoop", 1f);
                break;

            case 4:
                tayAISpeechBubble.changeTextWithEffect("In this lesson, we’ll only be talking about the Counted Loop.");
                playVoiceRecording("05");
                break;

            case 5:
                tayAISpeechBubble.changeTextWithEffect("Let’s say we have a program that makes a ball bounce in the run area.");
                playVoiceRecording("06");
                Invoke("flickTailSpawnBall", 1.5f);
                /**************************************************************/
                /* ADD MAKE BALL w/ RED COLOR ARGUMENT ON DRAFTING BOARD HERE */
                /**************************************************************/
                break;

            case 6:
                tayAISpeechBubble.changeTextWithEffect("But we want a program that makes twenty balls bouncing around the run area.");
                playVoiceRecording("07");
                break;

            case 7:
                tayAISpeechBubble.changeTextWithEffect("We could put twenty of these \"Make Ball\" blocks on the drafting board,");
                playVoiceRecording("08");
                break;

            case 8:
                tayAISpeechBubble.changeTextWithEffect("but that would take a long time and make our program look messy.");
                playVoiceRecording("09");
                break;

            case 9:
                tayAISpeechBubble.changeTextWithEffect("Luckily, we can do this really easily with a loop!");
                playVoiceRecording("10");
                Invoke("spawnInteractableCountLoop", 1.2f);
                tayAISpeechBubble.changeTextWithEffect("Go ahead and put this Counting Loop onto the drafting board.");
                playVoiceRecording("11");
                break;

            case 10:
                tayAISpeechBubble.changeTextWithEffect("Now that we have our loop on the board, we need to tell the loop how many times it should repeat.");
                playVoiceRecording("12");
                break;

            case 11:
                tayAISpeechBubble.changeTextWithEffect("Increase that 1 on the loop to 20; this is called the loop counter.");
                playVoiceRecording("13");
                Invoke("spawnLoopCounterLabel", 1f);
                break;

            case 12:
                tayAISpeechBubble.changeTextWithEffect("Good!");
                playVoiceRecording("14");
                break;

            case 13:
                tayAISpeechBubble.changeTextWithEffect("Right now our loop is empty.");
                playVoiceRecording("15");
                break;

            case 14:
                tayAISpeechBubble.changeTextWithEffect("We need to tell the loop what it needs to do twenty times.");
                playVoiceRecording("16");
                break;

            case 15:
                tayAISpeechBubble.changeTextWithEffect("Put this \"Make Ball\" block into the empty slot on the loop.");
                playVoiceRecording("17");
                Invoke("spawnMakeBallBlock", 1f);
                break;

            case 16:
                tayAISpeechBubble.changeTextWithEffect("This is called the \"Loop Body.\"");
                playVoiceRecording("18");
                Invoke("spawnLoopBodyLabel", 1f);
                break;

            case 17:
                tayAISpeechBubble.changeTextWithEffect("Fantastic! Now all we need to do is run the program!");
                playVoiceRecording("19");
                Invoke("spawnStartBlock", 1f);
                /* Run button appears and 20 balls bounce in run scene */
                break;

            case 18:
                tayAISpeechBubble.changeTextWithEffect("Now write a program on your own that makes 2 foxes in the run area.");
                playVoiceRecording("20");
                GameObject.FindGameObjectWithTag("Compile").GetComponent<objectSpawner>().spawnCount = 2;
                GameObject.FindGameObjectWithTag("Compile").GetComponent<objectSpawner>().spawnObject = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/CALObjects/Foxes/Brown Fox.prefab", typeof(GameObject));;
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
