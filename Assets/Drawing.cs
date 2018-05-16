using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

    public bool freeDraw = false;
    public GameObject PencilTip;
    public GameObject Pencil;
    public GameObject ZLOCK;
    public bool lockZ = false;
    public bool lockY = true;
    public bool lockX = false;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private LockZ ZLockScript;
    private SimpleGrab simpleGrabScript;
    private int capturedPencilID = 0;
    

    private void Start()
    {
        try
        {
            ZLockScript = ZLOCK.GetComponent<LockZ>();
            trackedObject = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)trackedObject.index);
            simpleGrabScript = gameObject.GetComponent<SimpleGrab>();
        }
        catch
        {
            Start();
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
        if (simpleGrabScript && simpleGrabScript.objectInHand && simpleGrabScript.objectInHand.name.Contains("pencil"))
        {
            if (Pencil && simpleGrabScript.objectInHand && simpleGrabScript.objectInHand.GetInstanceID() == capturedPencilID)
                goto skipDisIf;
            capturedPencilID = simpleGrabScript.objectInHand.GetInstanceID();
            try
            {
                Debug.Log("New Pencil");
                Pencil = simpleGrabScript.objectInHand;
                PencilTip = simpleGrabScript.objectInHand.GetComponentInChildren<DrawableArea>().PencilTip;
                GameObject moveFromDraw = GameObject.Find("MoveFromDraw");
                PencilTip.GetComponent<DrawableArea>().objectToMoveFromDrawing = moveFromDraw;
                moveFromDraw.GetComponent<MoveFromDrawing>().pencilTip = PencilTip;
            }
            catch
            {
                Debug.Log("Failed to init pencil in Drawing.cs");
            }
        }
        else {
            return;
        }
        skipDisIf:
        if (trackedObject == null || device == null)
            Start();

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && PencilTip.GetComponent<DrawableArea>().insideDrawArea || device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad) && freeDraw)
        {
            Reset();
        }

        if (ZLockScript.controllerEntered)
        {
            if(lockZ)
                Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            else if(lockY)
                Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            else if(lockX)
                Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            Debug.Log("Controller Recognized in ZLOCK - successful pencil ZLOCK");
        }
        else
        {
            Pencil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }
}
