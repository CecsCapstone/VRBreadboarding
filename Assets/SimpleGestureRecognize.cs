using UnityEngine;
using System.Collections;
using Leap;

public class SimpleGestureRecognize : MonoBehaviour {
    public Controller controller;
    public Frame frame;
	// Use this for initialization
	
    void Start () {
        controller = new Controller();
        controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
        controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
        controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
	}
	
	// Update is called once per frame
	void Update () {
        frame = controller.Frame();
        GestureList gestures = frame.Gestures();
        if(gestures.Count != 0)
        {
            foreach (Gesture gest in gestures)
			{
				if(gest.Type == Gesture.GestureType.TYPE_KEY_TAP)
				{
					Debug.Log (gest.Hands.Frontmost.Fingers[1].TipPosition);
					Leap.Vector fingerPosition = gest.Hands.Frontmost.Fingers[1].TipPosition;
					float x = fingerPosition.x;
					float y = fingerPosition.y;
					float z = fingerPosition.z;
					Vector3 position = new Vector3(x,y,z);
					Collider[] close_things =
						Physics.OverlapSphere(position.normalized, 2, -1);
					
					GameObject closest = null;
					float closestDistance = Mathf.Infinity;
					GrabbableObject[] listOfGrabbableObjects= new GrabbableObject[10];
					GameObject[] listOfGameObjects = GameObject.FindGameObjectsWithTag("GrabbableObject");

					

					//float closest_sqr_distance = 2 * 2;
					
					for (int j = 0; j < listOfGameObjects.Length; ++j) {
						if(listOfGameObjects[j] != null)
						{
							float dist = Vector3.Distance (position.normalized, listOfGameObjects[j].transform.position);
							if(closestDistance > dist)
							{
								closest = listOfGameObjects[j];
								closestDistance = dist;
							}
						}
					}
				
					Debug.Log (closest.name.ToString());
				}
			}
                //Debug.Log(gest.Type.ToString());
        }
        
	}
}
