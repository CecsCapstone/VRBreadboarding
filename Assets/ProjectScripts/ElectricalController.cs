using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricalController : MonoBehaviour 
{

    VoltageSourceObject voltageSource;
    List<GameObject> componentsPath;
    TargetController currentTarget;
    TargetController previousTarget;
    TargetController voltageTarget;
    List<Connector> currentTargetConnectors;
    Connector power;
    Connector ground;
    bool isTurnedOn; 
    int iterations = 0;

	// Use this for initialization
	void Start () 
    {
        voltageSource = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSourceObject>();
        voltageTarget = GameObject.FindGameObjectWithTag("VoltageTarget").GetComponent<TargetController>();
        currentTargetConnectors = voltageTarget.connectors;
        power = currentTargetConnectors[0];
        ground = currentTargetConnectors[1];
        

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
        componentsPath = new List<GameObject>();
        //currentTargetConnectors = power.end.connectors;
        previousTarget = power.end;
        if(previousTarget.instantiated != null)
        {
            componentsPath.Add(previousTarget.instantiated);
        }
        if( previousTarget.connectors[1].end != previousTarget)
        {
            currentTarget = previousTarget.connectors[1].end;
        }
        else 
        {
            currentTarget = previousTarget.connectors[1].start;
        }
        while (currentTarget != voltageTarget && iterations < 20)
        {
            foreach (var connector in currentTarget.connectors)
            {
                if (connector.start != previousTarget && connector.end != previousTarget)
                {
                    if (currentTarget.instantiated != null)
                    {
                        componentsPath.Add(currentTarget.instantiated);
                    }
                    if (connector.start == currentTarget)
                    {
                        previousTarget = currentTarget;
                        currentTarget = connector.end;
                    }
                    else
                    {
                        previousTarget = currentTarget;
                        currentTarget = connector.start;
                    }
                   
                }
            }
            iterations++;
        }

        foreach(var comp in componentsPath)
        { Debug.Log(comp.name); }
        Debug.Log(componentsPath);
        iterations = 0;
        
    }
}
