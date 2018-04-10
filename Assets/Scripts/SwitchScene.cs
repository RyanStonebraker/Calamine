using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SwitchScene : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Controller"))
            SceneManager.LoadScene("freeworld");
    }
}
