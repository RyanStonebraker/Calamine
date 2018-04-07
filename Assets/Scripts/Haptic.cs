using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public GameObject pencilTip;
    private DrawableArea pencilTipScript;

    private void Start()
    {
        if(pencilTip)
            pencilTipScript = pencilTip.GetComponent<DrawableArea>();

        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (!trackedObject)
            Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        device.TriggerHapticPulse(1000);
    }

    private void OnTriggerStay(Collider other)
    {
        if(pencilTip && pencilTipScript.insideDrawArea)
            device.TriggerHapticPulse(1000);
    }

    private void OnTriggerExit(Collider other)
    {
        device.TriggerHapticPulse(2000);
    }
}
