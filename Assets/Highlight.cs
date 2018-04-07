using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

    public Shader originalShader;

    private bool objectIsHighlighted(Collider other)
    {
        return other.gameObject.GetComponent<Renderer>().material.shader == Shader.Find("Unlit/Highlight");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Tool" && !objectIsHighlighted(other))
        {
            originalShader = other.gameObject.GetComponent<Renderer>().material.shader;
            other.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Highlight");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Tool" && originalShader)
        {
            other.gameObject.GetComponent<Renderer>().material.shader = originalShader;
            originalShader = null;
        }
    }
}
