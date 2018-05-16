using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CALObjCreator : MonoBehaviour {
	private CALSourceFile sourceFile;
	CALBlockProcessor blockProc;
	private bool createdMyObj;

	// Use this for initialization
	void Start () {
		blockProc = new CALBlockProcessor();
		sourceFile = new CALSourceFile();
		sourceFile.ReadSourceFile();
	}
	
	void OnCollisionEnter(Collision aCollision)
	{
		string testStr = "     BLOCKDEF(BEHAVIOR) int CountDivisible3(int rangeStart, int rangeEnd)     ";
		blockProc.ProcessNextLine(ref testStr);

		return;

		//string newMeshPath;
		//string newPrefabPath;
		//GameObject newCube;
		//Mesh newMesh;
		//MeshFilter newMeshFilter;
		//Transform newTransform;
		//GameObject newPrefab;

		///*Only do this once*/
		//if (createdMyObj)
		//	return;
		//else
		//	createdMyObj = true;

		///*Make the folders for the runtime assets*/
		////AssetDatabase.CreateFolder ("Assets/CollinDungeon", "RuntimeMeshes");
		////AssetDatabase.CreateFolder ("Assets/CollinDungeon", "RuntimePrefabs");

		//newMeshPath = "Assets/CollinDungeon/RTMesh.mesh";
		//newPrefabPath = "Assets/CollinDungeon/RTPrefab.prefab";
		////AssetDatabase.DeleteAsset (newMeshPath);
		////AssetDatabase.DeleteAsset (newPrefabPath);

		//newCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		//newCube.transform.position = new Vector3(0, 0.5f, 0);
		//newMeshFilter = (MeshFilter)newCube.GetComponent("MeshFilter");
		//newMesh = newMeshFilter.sharedMesh;

		//AssetDatabase.CreateAsset (newMesh, newMeshPath);
		//AssetDatabase.SaveAssets ();
		//AssetDatabase.Refresh ();

		//newTransform = newCube.transform;

		//newPrefab = PrefabUtility.CreatePrefab (newPrefabPath, newTransform.gameObject);



		///*Make a new cube from our prefab*/
		//GameObject newNewCube = Instantiate (newPrefab);
	}
}
