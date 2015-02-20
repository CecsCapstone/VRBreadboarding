using UnityEngine;
using System.Collections;
using Leap;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	public float threshhold = .05f;
	public GameObject selected = null;
	GameObject closest = null;

	// Use this for initialization
	void Start () {
		controller = new Controller();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
				frame = controller.Frame ();
				Leap.Vector fingerPosition = frame.Hands.Rightmost.Fingers[1].TipPosition;
				
				if (fingerPosition.Magnitude != 0) 
				{
						HandController controllerGO = GetComponent<HandController>();
						float x = (fingerPosition.x/1000)+controllerGO.transform.position.x;
						float y = (fingerPosition.y/1000)+controllerGO.transform.position.y;
						float z = -(fingerPosition.z/1000)+controllerGO.transform.position.z;
						Vector3 position = new Vector3 (x, y, z);
						Collider[] close_things = Physics.OverlapSphere (position, .1f, -1);
						float closestDistance = Mathf.Infinity;
						for (int j = 0; j < close_things.Length; ++j) {
								//Debug.Log ("Cube " + close_things [0].transform.position);
								if (close_things [j] != null && close_things[j].tag == "GrabbableObject") 
								{
									float dist = Vector3.Distance (position, close_things [j].transform.position);
									//Debug.Log ("Cube " + close_things [0].transform.position);
									if (closestDistance > dist) 
									{
										closest = close_things [j].gameObject;
										closestDistance = dist;
									}
								}
						}
						if(closest != null)
							HandleLights(closest, position);
				}
		}

		void HandleLights(GameObject closest, Vector3 position)
		{
			if (selected == null && Vector3.Distance(closest.transform.position, position) < threshhold) {
				closest.GetComponent<SelectedObject>().TurnOnLight();
				if (GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching()) {
					// Find all other grabbable objects that are and turn off their lights
					GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag("GrabbableObject");
					for (int i = 0; i < grabbableObjects.Length; i++)
					{
						grabbableObjects[i].GetComponent<SelectedObject>().TurnOffLight();
					}
					
					// Turn on selected object light
					selected = closest;
					closest.GetComponent<SelectedObject>().Select();
				}
			}
			else if (selected == null && Vector3.Distance(closest.transform.position, position) >= threshhold)
			{
				GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag("GrabbableObject");
				for (int i = 0; i < grabbableObjects.Length; i++)
				{
					grabbableObjects[i].GetComponent<SelectedObject>().TurnOffLight();
				}
			}
			else if(selected != null && Vector3.Distance(closest.transform.position, position) >= threshhold && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching())
			{
				selected.GetComponent<SelectedObject>().Deselect();
				selected = null;
			}
			else if(selected != null && Vector3.Distance(closest.transform.position, position) < threshhold && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching())
			{
				selected.GetComponent<SelectedObject>().Deselect();
				selected = closest;
				selected.GetComponent<SelectedObject>().Select();
			}
		}
}
