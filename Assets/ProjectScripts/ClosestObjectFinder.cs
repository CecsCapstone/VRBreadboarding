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
        controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
	}

    public GameObject ClosestItem()
    {
        List<Collider> close_things = new List<Collider>(Physics.OverlapSphere(position, .1f, -1));

        close_things.RemoveAll(col => col.name.StartsWith("bone") || col.GetComponent<Connector>() != null);
        float closestDistance = Mathf.Infinity;
        foreach(var item in close_things)
        {
            if (item != null && item.tag == "GrabbableObject")
            {
                float dist = Vector3.Distance(position, item.transform.position);
                if (closestDistance > dist)
                {
                    closest = item.gameObject;
                    closestDistance = dist;
                }
            }
        }
        return closest;
    }

    public GameObject ClosestTarget()
    {
        List<Collider> close_things = new List<Collider>(Physics.OverlapSphere(position, .1f, -1));

        close_things.RemoveAll(col => col.name.StartsWith("bone") || col.GetComponent<Connector>() != null);
        float closestDistance = Mathf.Infinity;
        foreach(var item in close_things)
        {
            if (item != null && item.tag == "Target")
            {
                float dist = Vector3.Distance(position, item.transform.position);
                if (closestDistance > dist)
                {
                    closest = item.gameObject;
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
        if (frame.Gestures().Count > 0 && selected != null && selected.GetComponent<ConnectorController>() != null && selected.GetComponent<ConnectorController>().start != null)
        {
            selected.GetComponent<ConnectorController>().Reset();
        }


        position = GetTipPosition();

        closest = ClosestItem();
		if(closest != null && closest.GetComponent<SelectedObject>() != null)
        {
            SelectMaybe(closest, position);
        }

        //if (selected != null && Vector3.Distance(closest.transform.position, position) >= threshhold && GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1))
        //{
        //    selected.GetComponent<SelectedObject>().Deselect();
        //    GetComponent<HandController>().GetComponent<TargetSelectController>().enabled = false;
        //    if (selected.GetComponent<ConnectorController>() != null && selected.GetComponent<ConnectorController>().start != null)
        //    {
        //        selected.GetComponent<ConnectorController>().Reset();
        //    }
        //    selected = null;
        //}
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
            if (selected.GetComponent<ConnectorController>() != null && selected.GetComponent<ConnectorController>().start != null)
            {
                selected.GetComponent<ConnectorController>().Reset();
            }
            Select(closest);
		}
	}

    public void Select(GameObject closest)
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

    private Vector3 GetTipPosition()
    {
        frame = controller.Frame();
        Leap.Vector fingerposition = frame.Hands.Rightmost.Fingers[1].TipPosition;
        HandController controllerGO = GetComponent<HandController>();

        Vector3 local_tip = fingerposition.ToUnityScaled();

        return controllerGO.transform.TransformPoint(local_tip);
    }
}
