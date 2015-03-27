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
    bool isTurnedOn; 

	// Use this for initialization
	void Start () 
    {
        voltageSource = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSourceObject>();
        currentTargetConnectors = GameObject.FindGameObjectWithTag("VoltageTarget").GetComponent<TargetController>().connectors;
        power = currentTargetConnectors[0];
        ground = currentTargetConnectors[1];
        currentTargetConnectors = power.end.connectors;
        isTurnedOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

	}

    public void TurnOn()
    {
        isTurnedOn = true;
        Analyze();
    }

    public void TurnOff()
    {
        isTurnedOn = false;
    }

    public bool IsTurnedOn()
    {
        return isTurnedOn;
    }

    private void Analyze()
    {
    }
}
