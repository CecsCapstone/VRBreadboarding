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
                Debug.Log(gest.Type.ToString());
        }
        
	}
}
