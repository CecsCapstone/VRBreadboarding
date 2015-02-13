using UnityEngine;
using System.Collections;
using Leap;

public class ClosestObjectFinder : MonoBehaviour {
	public Controller controller;
	public Frame frame;
	GameObject closest = null;
	public float grabTriggerDistance = 0.7f;
	GameObject selected = null;

	// Use this for initialization
	void Start () {
		controller = new Controller();

	}

	protected bool IsPinching () {
		HandModel hand_model = GetComponent<HandModel>();
		Hand leap_hand = hand_model.GetLeapHand();
		
		Vector leap_thumb_tip = leap_hand.Fingers[0].TipPosition;
		float closest_distance = Mathf.Infinity;
		
		// Check thumb trip distance to joints on all other fingers.
		// If it's close enough, you're pinching.
		for (int i = 1; i < HandModel.NUM_FINGERS; ++i) {
			Finger finger = leap_hand.Fingers[i];
			
			for (int j = 0; j < FingerModel.NUM_BONES; ++j) {
				Vector leap_joint_position = finger.Bone((Bone.BoneType)j).NextJoint;
				
				float thumb_tip_distance = leap_joint_position.DistanceTo(leap_thumb_tip);
				closest_distance = Mathf.Min(closest_distance, thumb_tip_distance);
			}
		}
		
		// Scale trigger distance by thumb proximal bone length.
		float proximal_length = leap_hand.Fingers[0].Bone(Bone.BoneType.TYPE_PROXIMAL).Length;
		float trigger_distance = proximal_length * grabTriggerDistance;

		if (closest_distance <= trigger_distance) {
			return true;
		} else {
			return false;
		}
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
								light.intensity = 2;
								if (IsPinching()){
									selected = closest;
									light.color = UnityEngine.Color.green;
								} else {
									selected = null;
									light.intensity = 0;
								}
						} else {
								closest.light.intensity = 0;
						}
					
				}
		}


}
