using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using Hovercast.Core.Custom;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	public float threshhold = .05f;
	public AudioSource audio;
	public GameObject selected = null;
	GameObject closest = null;
    Vector3 position;
	

	// Use this for initialization
	void Start () {
		controller = new Controller();
        controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
		audio.Play();
	}

    void FixedUpdate()
    {
        position = GetTipPosition();
        if (selected == null)
        {
            if (GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1))
            {
                GameObject closestItem = ClosestItem();
                if (closestItem != null)
                {
                    if (closestItem.GetComponent<Connector>() != null)
                    {
                        Connector connector = closestItem.GetComponent<Connector>();
                        connector.start.RemoveConnector(connector);
                        connector.end.RemoveConnector(connector);
                        
                    }
                    Destroy(closestItem);
                }
            }
        }
    }

    public GameObject ClosestItem()
    {
		List<Collider> close_things = getUsefulCloseThings();
        float closestDistance = Mathf.Infinity;
        foreach(var item in close_things)
        {
            if (item != null && item.GetComponent<SelectedObject>() != null && item.GetComponent<SelectedObject>().enabled == false)
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

    public TargetController ClosestTarget()
    {
        List<Collider> close_things = getUsefulCloseThings();
		if(close_things!=null)
		{	
			GameObject closestTarget = null;
			float closestDistance = Mathf.Infinity;
			foreach(var item in close_things)
			{
			    if (item != null && item.tag == "Target")
			    {
			        float dist = Vector3.Distance(position, item.transform.position);
			        if (closestDistance > dist)
			        {
			            closestTarget = item.gameObject;
			            closestDistance = dist;
			        }
			    }
			}
			if(closestTarget == null)
				return null;

			return closestTarget.GetComponent<TargetController>();
		}
		return null;
    }
	
    public void Select(GameObject closest)
    {
        selected = closest;
        GetComponent<HandController>().GetComponent<TargetSelectController>().enabled = true;
        selected.GetComponent<SelectedObject>().Select();
    }

    private Vector3 GetTipPosition()
    {
        frame = controller.Frame();
        Leap.Vector fingerposition = frame.Hands.Rightmost.Fingers[1].TipPosition;
        HandController controllerGO = GetComponent<HandController>();

        Vector3 local_tip = fingerposition.ToUnityScaled();

        return controllerGO.transform.TransformPoint(local_tip);
    }

	private List<Collider> getUsefulCloseThings() 
	{
		List<Collider> close_things = new List<Collider>(Physics.OverlapSphere(position,threshhold,-1));
		close_things.RemoveAll(col => col.name.StartsWith("bone") || col.GetComponent<Connector>() != null || col.name.StartsWith("palm")|| col.tag.Contains("nonCloseThings"));
		return close_things;
	}
}
