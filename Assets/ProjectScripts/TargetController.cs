using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {
	public Light light;
	GameObject controller;
    GameObject gameInstantiated;
	GameObject instantiated;

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
                        if (connector.start == null)
                        {
                            connector.start = this;
                        }
                        else if (connector.end == null && this != connector.start)
                        {
                            connector.end = this;
                        }
                    }
                }
			}
		}
	}
}
