using UnityEngine;
using System.Collections;
using Leap;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	GameObject closest = null;
	GameObject selected = null;

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
						//Debug.Log (controllerGO.transform.position);
						
						float x = (fingerPosition.x/1000)+controllerGO.transform.position.x;
						float y = (fingerPosition.y/1000)+controllerGO.transform.position.y;
						float z = -(fingerPosition.z/1000)+controllerGO.transform.position.z;
						

						Vector3 position = new Vector3 (x, y, z);
						Debug.Log (position);
						Collider[] close_things = Physics.OverlapSphere (position, .5f, -1);
						//Debug.Log("go: " + go[0].transform.position);
		
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
						if (Vector3.Distance(closest.transform.position, position) < 0.1f) {
								Debug.Log (closest.name.ToString ());
								Light light = closest.light;
                                light.color = UnityEngine.Color.red;
								light.intensity = 2;
								if (GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching()) {
									selected = closest;
									selected.light.color = UnityEngine.Color.green;
								} else if(selected.light.color != UnityEngine.Color.red) {
									selected = null;
									light.intensity = 0;
								}
						} else if(selected != null && closest.light.color != UnityEngine.Color.green) {
								closest.light.intensity = 0;
						}
					
				}
		}


}
