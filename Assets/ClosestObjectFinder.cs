using UnityEngine;
using System.Collections;
using Leap;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	public InteractionBox iBox;

	// Use this for initialization
	void Start () {
		controller = new Controller();
		iBox = new InteractionBox ();
		iBox.Center.x = -0.5f;
		iBox.Center.y = .891f;
		iBox.Center.z = -.447f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
				frame = controller.Frame ();

		Leap.Vector fingerPosition = frame.Hands.Frontmost.Fingers.Frontmost.TipPosition;
				
				if (fingerPosition.Magnitude != 0) 
				{
					Leap.Vector fingerNormalized = leapToWorld (frame.Hands.Frontmost.Fingers.Frontmost.TipPosition,iBox);
						
						float x = fingerPosition.x;
						float y = fingerPosition.y;
						float z = fingerPosition.z;
						

						Vector3 position = new Vector3 (x, y, z);
						Debug.Log (fingerNormalized);
						Collider[] close_things = Physics.OverlapSphere (position, 20f, -1);
						GameObject[] go = GameObject.FindGameObjectsWithTag("GrabbableObject");
						Debug.Log("go: " + go[0].transform.position);
		
						GameObject closest = null;
						float closestDistance = Mathf.Infinity;
					
						for (int j = 0; j < close_things.Length; ++j) {
								Debug.Log ("Cube " + close_things [0].transform.position);
								if (close_things [j] != null && close_things[j].tag == "GrabbableObject") 
								{
									float dist = Vector3.Distance (position.normalized, close_things [j].transform.position);
									Debug.Log ("Cube " + close_things [0].transform.position);
									if (closestDistance > dist) 
									{
										closest = close_things [j].gameObject;
										closestDistance = dist;
									}
								}
						}
						if (closestDistance < 0.2f) {
								Debug.Log (closest.name.ToString ());
						}
					
				}
		}

	Leap.Vector leapToWorld(Leap.Vector leapPoint, InteractionBox iBox)
	{
		leapPoint.z *= -1.0f; //right-hand to left-hand rule
		Leap.Vector normalized = iBox.NormalizePoint(leapPoint, false);
		normalized += new Leap.Vector(0.5f, 0f, 0.5f); //recenter origin
		return normalized; //scale
	}
}
