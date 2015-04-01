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
        if (placingObject.GetComponent<LED>() == null)
        {
            instantiated = Instantiate(placingObject, transform.position, Quaternion.Euler(placingObject.transform.localEulerAngles.x, placingObject.transform.localEulerAngles.y, placingObject.transform.localEulerAngles.z)) as GameObject;
        }
        else
        {
            Vector3 LEDPosition = new Vector3(transform.position.x - .015f, transform.position.y - .09f, transform.position.z - .008f);
            instantiated = Instantiate(placingObject, LEDPosition, Quaternion.Euler(placingObject.transform.localEulerAngles.x, placingObject.transform.localEulerAngles.y, placingObject.transform.localEulerAngles.z)) as GameObject;
        }
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
