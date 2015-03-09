using UnityEngine;
using System.Collections.Generic;

public class TargetController : MonoBehaviour {

    public Light light;
    public GameObject wirePrefab;
	GameObject controller;
    GameObject gameInstantiated;
	GameObject instantiated;
    ConnectorController connectorController;
    List<Connector> connectors;
    int connectorIndex = 0;

	// Use this for initialization
	void Start () 
	{
		light = GetComponent<Light> ();
		controller = GameObject.FindGameObjectWithTag ("HandController");
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
                    ConnectorController connector = selected.GetComponent<ConnectorController>();
                    if (connector == null)
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
                        /*Connector newConnector = connector;
                        connectors.Add(Instantiate(newConnector, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as Connector);
                        if (connectors[connectorIndex].start == null)
                        {
                            connectors[connectorIndex].start = this;
                        }
                        else if (connectors[connectorIndex].end == null && this != connectors[connectorIndex].start)
                        {
                            connectors[connectorIndex].end = this;
                            connectors[connectorIndex].PlaceWire(wirePrefab);
                            connectorIndex++;
                        }*/
                        if (connectorController.start == null)
                        {
                            connectorController.start = this;
                        }
                        else if (connectorController.end == null && this != connectorController.start)
                        {
                            connectorController.end = this;
                            Connector newConnector = connectorController.PlaceWire(wirePrefab);
                            connectors.Add(newConnector);
                            connectorIndex++;
                        }
                    }
                }
			}
		}
	}
}
