using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    public bool switchScene = false;

    void OnCollisionEnter(Collision collision)
    {
        switchScene = true;
        SceneManager.LoadScene("freeworld");
    }
}
