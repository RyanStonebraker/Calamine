using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Runtime.InteropServices;

public class PluginUse : MonoBehaviour {

    [DllImport("CPPPlugin", EntryPoint = "RandomIntBounded")]
    public static extern int RandomIntBounded(int lowBound, int highBound);

	// Use this for initialization
	void Start () {
        int randomNumber;

        randomNumber = RandomIntBounded(4, 20);
        Debug.Log("The random number is: " + randomNumber);
	}
}
