using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CompilerUI : MonoBehaviour {

	public bool hasBeenHit = false;
    public GameObject hardCodedObj;
	GameObject creationObj;
	Vector3 creationObjPosition;
	Transform creationObjTransform;
	GameObject creationPrefab;
	string newPrefabPath = "Assets/CollinDungeon/NewEnv.prefab";
    public Board pegBoard = null;


    // Use this for initialization
    void Start () {
		//creationObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		creationObjPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Debug.Log("*****************************FIRE AT TRISTAN*****************************");
	}

	void OnCollisionEnter(Collision aCollision)
	{
        Debug.Log("*****************************FIRE AT TRISTAN, PLEASE*****************************");
        if (hasBeenHit)
			return;

		hasBeenHit = true;
        Board pegBoard = GameObject.Find("PlayerBoard").GetComponent<Board>();
        //Board pegBoard = GameObject.FindWithTag("CollectiveBoard").GetComponent<Board>();
		pegBoard.SetShouldYieldView (true);
		this.gameObject.GetComponent<Renderer>().enabled = false;
        creationObjPosition = new Vector3(301.0f, 2.16f, 251.23f);
        //creationObjTransform = creationObj.transform;
        //creationPrefab = PrefabUtility.CreatePrefab (newPrefabPath, creationObjTransform.gameObject);
        List<GameObject> nodeData = pegBoard.getList();
        Invoke("TimerFireAction", 5.0f);

		for (int i = 0; i < nodeData.Count; i++) {
			GameObject theObj;
			Node 	   theNode;

            theObj = nodeData[i];
            Debug.Log("string, yarn, twine");
			theNode = theObj.GetComponent<Node>();
			if(theNode.collidingObject != null) {
                //Invoke("TimerFireAction", 2.5f);
                TimerFireAction();
                Debug.Log("This is a helpful debug statement.\nYou can tell because it has a period at the end.");
				break;
			}
		}
	}

	void TimerFireAction()
	{
        hardCodedObj = Instantiate(hardCodedObj, hardCodedObj.transform.position, hardCodedObj.transform.rotation) as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
