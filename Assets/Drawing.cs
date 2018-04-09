using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    public bool freeDraw = false;
    public GameObject PencilTip;
    public GameObject Pencil;
    public GameObject ZLOCK;
    private LockZ ZLockScript;

    private void Start()
    {
        try
        {
            ZLockScript = ZLOCK.GetComponent<LockZ>();
            trackedObject = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)trackedObject.index);
        }
        catch
        {
            Debug.Log("Init failed in draw start");
        }
    }

    private void Reset()
    {
        PencilTip.gameObject.GetComponent<DrawableArea>().vertices = null;
        PencilTip.gameObject.GetComponent<TrailRenderer>().Clear();
        PencilTip.gameObject.GetComponent<TrailRenderer>().enabled = !PencilTip.gameObject.GetComponent<TrailRenderer>().enabled;
    }

    private void Update()
    {
        if (trackedObject == null || device == null)
            Start();

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && PencilTip.GetComponent<DrawableArea>().insideDrawArea || device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && freeDraw)
        {
            Reset();
        }

        if (ZLockScript.controllerEntered)
        {
            Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            Debug.Log("Controller Recognized in ZLOCK - successful pencil ZLOCK");
        }
        else
        {
            Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }
}
