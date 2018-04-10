using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SwitchScene : MonoBehaviour {

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name.Contains("Controller"))
        {
            Debug.Log("Scene Switch");
            Invoke("reload", 1f);
        }
            
    }
}
