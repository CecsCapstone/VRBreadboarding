using UnityEngine;
using System.Collections;
using Leap;

public class InitializationController : MonoBehaviour {

    public static InitializationController SP;
    public Controller leapMotionController;
    public Frame currentFrame;
    public GestureList gestures;
    public HandList hands;
    public FingerList fingers;
    public PointableList pointables;
    public ToolList tools;

    /*public FrameListner frameListner;
    private class FrameListner : Listener
    {
        private void onFrame(Controller leapMotionController)
        {  
            Frame frame = leapMotionController.Frame();
        }
    }*/

    private void Awake()
    {
        SP = this;
    }

	private void Start()
    {
        InitializeControllers();
        EnableGestures();
	}

    private void FixedUpdate()
    {
        currentFrame = leapMotionController.Frame();
        gestures = currentFrame.Gestures();
        hands = currentFrame.Hands;
        fingers = currentFrame.Fingers;
        tools = currentFrame.Tools;
    }

    private void EnableGestures()
    {
        leapMotionController.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
        leapMotionController.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
        leapMotionController.EnableGesture(Gesture.GestureType.TYPESWIPE);
        leapMotionController.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
    }

    private void InitializeControllers()
    {
        leapMotionController = new Controller();
    }
}
