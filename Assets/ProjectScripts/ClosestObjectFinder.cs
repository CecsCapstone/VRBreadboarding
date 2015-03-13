using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	public float threshhold = .05f;
	public GameObject selected = null;
	GameObject closest = null;

    Vector3 position;

	// Use this for initialization
	void Start () {
		controller = new Controller();
	}

    public GameObject ClosestItem()
    {
        frame = controller.Frame();
        Leap.Vector fingerPosition = frame.Hands.Rightmost.Fingers[1].TipPosition;
        HandController controllerGO = GetComponent<HandController>();

        float x = (fingerPosition.x / 1000) + controllerGO.transform.position.x;
        float y = (fingerPosition.y / 1000) + controllerGO.transform.position.y;
        float z = -(fingerPosition.z / 1000) + controllerGO.transform.position.z;

        position = new Vector3(x, y, z);
        List<Collider> close_things = new List<Collider>(Physics.OverlapSphere(position, .1f, -1));

        close_things.RemoveAll(col => col.name.StartsWith("bone"));
        float closestDistance = Mathf.Infinity;
        for (int j = 0; j < close_things.Count; ++j)
        {
            //Debug.Log ("Cube " + close_things [0].transform.position);
            Connector connector = close_things[j].GetComponent<Connector>();
            if (close_things[j] != null && close_things[j].tag == "GrabbableObject" && connector == null)
            {
                float dist = Vector3.Distance(position, close_things[j].transform.position);
                //Debug.Log ("Cube " + close_things [0].transform.position);
                if (closestDistance > dist)
                {
                    closest = close_things[j].gameObject;
                    closestDistance = dist;
                }
            }
        }
        return closest;
    }

    public GameObject ClosestTarget()
    {
        //frame = controller.Frame();
        //Leap.Vector fingerPosition = frame.Hands.Rightmost.Fingers[1].TipPosition;
        //HandController controllerGO = GetComponent<HandController>();

        //float x = (fingerPosition.x / 1000) + controllerGO.transform.position.x;
        //float y = (fingerPosition.y / 1000) + controllerGO.transform.position.y;
        //float z = -(fingerPosition.z / 1000) + controllerGO.transform.position.z;

        //position = new Vector3(x, y, z);
        List<Collider> close_things = new List<Collider>(Physics.OverlapSphere(position, .1f, -1));

        close_things.RemoveAll(col => col.name.StartsWith("bone"));
        float closestDistance = Mathf.Infinity;
        for (int j = 0; j < close_things.Count; ++j)
        {
            //Debug.Log ("Cube " + close_things [0].transform.position);
            Connector connector = close_things[j].GetComponent<Connector>();
            if (close_things[j] != null && close_things[j].tag == "Target" && connector == null)
            {
                float dist = Vector3.Distance(position, close_things[j].transform.position);
                //Debug.Log ("Cube " + close_things [0].transform.position);
                if (closestDistance > dist)
                {
                    closest = close_things[j].gameObject;
                    closestDistance = dist;
                }
            }
        }
        return closest;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        frame = controller.Frame();
        Leap.Vector fingerPosition = frame.Hands.Rightmost.Fingers[1].TipPosition;
        HandController controllerGO = GetComponent<HandController>();

        float x = (fingerPosition.x / 1000) + controllerGO.transform.position.x;
        float y = (fingerPosition.y / 1000) + controllerGO.transform.position.y;
        float z = -(fingerPosition.z / 1000) + controllerGO.transform.position.z;

        position = new Vector3(x, y, z);

        closest = ClosestItem();
		if(closest != null && closest.GetComponent<SelectedObject>() != null)
        {
            SelectMaybe(closest, position);
        }

        if (selected != null && Vector3.Distance(closest.transform.position, position) >= threshhold && GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1))
        {
            selected.GetComponent<SelectedObject>().Deselect();
            GetComponent<HandController>().GetComponent<TargetSelectController>().enabled = false;
            selected = null;
        }
	}

	void SelectMaybe(GameObject closest, Vector3 position)
	{
        GameObject handModel = GameObject.FindGameObjectWithTag("HandModel");
		if (selected == null && Vector3.Distance(closest.transform.position, position) < threshhold) 
        {
			closest.GetComponent<SelectedObject>().TurnOnLight();
            if (handModel != null && handModel.GetComponent<IsPinching>().Pinching(1))
            {
                HandleLights();
				// Turn on selected object light
                Select(closest);
			}
		}
		else if (selected == null && Vector3.Distance(closest.transform.position, position) >= threshhold)
		{
               HandleLights();
		}
        else if (selected != null && Vector3.Distance(closest.transform.position, position) < threshhold && handModel != null && handModel.GetComponent<IsPinching>().Pinching(1))
		{
			selected.GetComponent<SelectedObject>().Deselect();
            Select(closest);
		}
	}

    private void Select(GameObject closest)
    {
        selected = closest;
        GetComponent<HandController>().GetComponent<TargetSelectController>().enabled = true;
        selected.GetComponent<SelectedObject>().Select();
    }

    private void HandleLights()
    {
        // Find all other grabbable objects that are and turn off their lights
        GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag("GrabbableObject");
        for (int i = 0; i < grabbableObjects.Length; i++)
        {
            grabbableObjects[i].GetComponent<SelectedObject>().TurnOffLight();
        }
    }
}
