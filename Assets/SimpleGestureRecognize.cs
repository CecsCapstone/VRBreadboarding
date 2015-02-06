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

					GrabbableObject closest = GetComponent<GrabbableObject>();
					float closestDistance = Mathf.Infinity;
					GrabbableObject[] listOfGameObjects = new GrabbableObject[10];
					listOfGameObjects = GetComponents<GrabbableObject>();

					for(int i = 0; i < 10; i++)
					{
						Debug.Log (i);
						if(listOfGameObjects[i] != null)
						{
							Debug.Log(listOfGameObjects[i]);
							float dist = Vector3.Distance (position, listOfGameObjects[i].transform.position);
							if(closestDistance > dist)
							{
								closest = listOfGameObjects[i];
							}
						}
					}
					Debug.Log (closest.ToString());
				}
			}
                //Debug.Log(gest.Type.ToString());
        }
        
	}
}
