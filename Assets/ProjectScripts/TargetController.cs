using UnityEngine;
using System.Collections.Generic;

public class TargetController : MonoBehaviour {

    public Light light;
	GameObject controller;
    GameObject gameInstantiated;
	GameObject instantiated;
    List<Connector> connectors;
    int connectorIndex = 0;

	// Use this for initialization
	void Start () 
	{
		light = GetComponent<Light> ();
		controller = GameObject.FindGameObjectWithTag ("HandController");
        connectors = new List<Connector>();
	}

    GameObject PlaceObject(GameObject placingObject)
    {
        instantiated = Instantiate(placingObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        instantiated.GetComponent<SelectedObject>().TurnOffLight();
        instantiated.GetComponent<Collider>().enabled = false;
        //instantiated.GetComponent<Rigidbody>().useGravity = false;
        instantiated.GetComponent<SelectedObject>().enabled = false;

        return instantiated;
    }

	// Update is called once per frame
	void FixedUpdate () 
	{
		Collider[] close_things = Physics.OverlapSphere (transform.position, .075f, -1);
		GameObject selected = null;
		for(int i = 0; i < close_things.Length; i++)
		{
			//Debug.Log(close_things[i].ToString());
			if(close_things[i].name == "palm")
			{
				selected = controller.GetComponent<ClosestObjectFinder>().selected;
                if (selected != null)
                {
                    ConnectorController connectorController = selected.GetComponent<ConnectorController>();
                    if (connectorController == null)
                    {
                        if (selected != null && instantiated == null)
                        {
                            light.intensity = 1;
                            instantiated = PlaceObject(selected);
                        }
                        else if (selected != null && selected.name != instantiated.name)
                        {
                            Destroy(instantiated);
                            instantiated = PlaceObject(selected);
                        }
                    }
                    else
                    {
                        if (connectorController.start == null)
                        {
                            connectorController.start = this;
                        }
                        else if (connectorController.end == null && this != connectorController.start)
                        {
                            connectorController.end = this;
                            Connector newConnector = connectorController.PlaceWire();
                            connectors.Add(newConnector);
                            connectorIndex++;
                        }
                    }
                }
			}
		}
	}
}
