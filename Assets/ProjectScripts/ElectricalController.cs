using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricalController : MonoBehaviour 
{

    VoltageSourceObject voltageSource;
    List<GameObject> componentsPath;
    List<Connector> currentTargetConnectors;
    Connector power;
    Connector ground;

	// Use this for initialization
	void Start () 
    {
        voltageSource = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSourceObject>();
        currentTargetConnectors = GameObject.FindGameObjectWithTag("VoltageTarget").GetComponent<TargetController>().connectors;
        power = currentTargetConnectors[0];
        ground = currentTargetConnectors[1];
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
}
