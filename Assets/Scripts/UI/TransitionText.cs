using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionText : MonoBehaviour {

    private UnityEngine.UI.Text textBox;
    private Animator animator;
    private string text;

    private void Start()
    {
        animator = GetComponent<Animator>();
        textBox = GetComponent<UnityEngine.UI.Text>();
    }

    public void changeTextWithEffect(string newText)
    {
        text = newText;
        animator.SetTrigger("ZoomTransition");
        Invoke("changeText", 0.5f);
    }

    private void changeText()
    {
        textBox.text = text;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Semicolon))
            changeTextWithEffect(":) TEST :)");
    }
}
