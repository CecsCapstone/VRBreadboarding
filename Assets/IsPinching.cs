using UnityEngine;
using System.Collections;
using Leap;

public class IsPinching : MonoBehaviour {
    public float grabTriggerDistance = 0.7f;

    public bool Pinching()
    {
        HandModel hand_model = GetComponent<HandModel>();
        Hand leap_hand = hand_model.GetLeapHand();

        Vector leap_thumb_tip = leap_hand.Fingers[0].TipPosition;
        float closest_distance = Mathf.Infinity;

        // Check thumb trip distance to joints on all other fingers.
        // If it's close enough, you're pinching.
        Finger finger = leap_hand.Fingers[1];

        for (int j = 0; j < FingerModel.NUM_BONES; ++j)
        {
            Vector leap_joint_position = finger.Bone((Bone.BoneType)j).NextJoint;

            float thumb_tip_distance = leap_joint_position.DistanceTo(leap_thumb_tip);
            closest_distance = Mathf.Min(closest_distance, thumb_tip_distance);
        }

        // Scale trigger distance by thumb proximal bone length.
        float proximal_length = leap_hand.Fingers[0].Bone(Bone.BoneType.TYPE_PROXIMAL).Length;
        float trigger_distance = proximal_length * grabTriggerDistance;

        if (closest_distance <= trigger_distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
