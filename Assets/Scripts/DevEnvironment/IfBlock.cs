using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IfBlock : MonoBehaviour
{
    public GameObject nodeToSpawn;
    public float xShiftOnCollide = 1.5f;
    public List<GameObject> nodeConnections = null;
    public string outputFilePath = "Assets/output.txt";

    public int numNodes = 0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacted");
        try
        {
            writeToOutputFile(gameObject.name + " If Block Event");
        }
        catch
        {
            Debug.Log("Bag impact, no rigidbody?");
        }
    }

    //FILE IO

    void writeToOutputFile(string data)
    {
        try
        {
            StreamWriter outFile = new StreamWriter(outputFilePath, true); //true = append, false = overwrite
            outFile.WriteLine(data);
            outFile.Close();
            Debug.Log("File Write Good");
        }
        catch
        {
            Debug.Log("File Write Failed");
        }
    }

    void Update()
    {
        if(nodeConnections[numNodes].GetComponent<IfNode>().collidingObject != null) //NEED TO ADD CONTROLLER EVENT HERE TO PREVENT ENDLESS SPAWNING
        {
            gameObject.transform.localScale += new Vector3(0, 12.5f, 0);
            gameObject.transform.position += new Vector3(0, 1.14f, 0);
            nodeConnections.Add(Instantiate(nodeToSpawn, nodeConnections[numNodes].transform.position, Quaternion.identity));
            for (int i = 0; i < numNodes+1; i++)
            {
                nodeConnections[i].transform.position += new Vector3(0, 2.3f, 0);
                nodeConnections[i].GetComponent<IfNode>().collidingObject.transform.position += new Vector3(0, 2.3f, 0);
            }

            numNodes++;
            nodeConnections[numNodes].transform.localScale = new Vector3(1f, 1f, 1f);/*nodeConnections[numNodes - 1].transform.localScale*/;
        }
    }
}
