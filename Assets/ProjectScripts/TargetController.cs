using UnityEngine;
using System.Collections.Generic;

public class TargetController : MonoBehaviour {

    public Light light;
	GameObject controller;
	public GameObject instantiated;
    public List<Connector> connectors;
    public int connectorIndex = 0;

	// Use this for initialization
	void Start () 
	{
		light = GetComponent<Light> ();
		controller = GameObject.FindGameObjectWithTag ("HandController");
        connectors = new List<Connector>();
	}

    public GameObject PlaceObject(GameObject placingObject)
    {
        instantiated = Instantiate(placingObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        instantiated.GetComponent<SelectedObject>().TurnOffLight();
        //instantiated.GetComponent<Collider>().enabled = false;
        //instantiated.GetComponent<Rigidbody>().useGravity = false;
        instantiated.GetComponent<SelectedObject>().enabled = false;

        return instantiated;
    }

    public void RemoveConnector(Connector connector)
    {
        connectors.Remove(connector);
    }
}
