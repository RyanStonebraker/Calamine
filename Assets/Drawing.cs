using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    public bool freeDraw = false;
    public GameObject PencilTip;

    private void Start()
    {
        try
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)trackedObject.index);
        }
        catch
        {
            Debug.Log("Init failed in draw start");
        }
    }

    private void Update()
    {
        if (trackedObject == null || device == null)
            Start();

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && PencilTip.GetComponent<DrawableArea>().insideDrawArea || device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && freeDraw)
        {
            PencilTip.gameObject.GetComponent<TrailRenderer>().Clear();
            PencilTip.gameObject.GetComponent<TrailRenderer>().enabled = !PencilTip.gameObject.GetComponent<TrailRenderer>().enabled;
        }
    }
}
