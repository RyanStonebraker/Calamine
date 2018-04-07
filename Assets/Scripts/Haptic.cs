using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    private void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    private void OnTriggerEnter(Collider other)
    {
        device.TriggerHapticPulse(500);
    }

    private void OnTriggerExit(Collider other)
    {
        device.TriggerHapticPulse(100);
    }
}
