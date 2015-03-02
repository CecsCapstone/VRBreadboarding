using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    private Connector connectorA;
	// Use this for initialization
	void Start () {
        connectorA = new Connector();
        Debug.Log(connectorA);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
