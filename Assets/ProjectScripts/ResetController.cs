using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRWidgets;

public class ResetController : MonoBehaviour {

	public List<TargetController> targets;
    public TargetController powerTarget;
    public TargetController groundTarget;
	public GameObject power;
	public GameObject ground;
    public ButtonDemoToggle onOff;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset()
	{
        List<GameObject> connectors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Connector"));
        connectors.Remove(ground);
        connectors.Remove(power);
        
		foreach(var target in targets)
		{
            if(target.instantiated != null)
            {
                Destroy(target.instantiated);
                target.instantiated = null;
            }
			target.connectors = new List<Connector>();
		}

        foreach (var connector in connectors)
        {
            Destroy(connector);
        }

        if(powerTarget.instantiated != null)
        {
            Destroy(powerTarget.instantiated);
            powerTarget.instantiated = null;
           
        }
        //foreach (var connector in powerTarget.connectors)
        //{
        //    if (connector != power.GetComponent<Connector>())
        //    {
        //        powerTarget.RemoveConnector(connector);
        //    }
        //}

        powerTarget.connectors = new List<Connector>();
        powerTarget.connectors.Add(power.GetComponent<Connector>());

        if (groundTarget.instantiated != null)
        {
            Destroy(groundTarget.instantiated);
            groundTarget.instantiated = null;
        }

        //foreach (var connector in groundTarget.connectors)
        //{
        //    if (connector != ground.GetComponent<Connector>())
        //    {
        //        groundTarget.RemoveConnector(connector);
        //    }
        //}
        groundTarget.connectors = new List<Connector>();
        groundTarget.connectors.Add(power.GetComponent<Connector>());

        onOff.TurnButtonOff();
	}
}
