using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManoClass
{
    NO_HAND = -1,
    GRAB_GESTURE_FAMILY = 0,
    PINCH_GESTURE_FAMILY = 1,
    POINTER_GESTURE_FAMILY = 2
}
;
public enum HandSide
{
    FRONT,
    BACK
}
;

public enum ManoGestureTrigger
{
    NO_GESTURE = -1,
    CLICK = 1,
    SWIPE_LEFT = 5,
    SWIPE_RIGHT = 6,
    GRAB = 4,
    TAP_POINTING = 2,
    DROP = 8,
    PICK = 7,
    RELEASE = 3
}
;

public enum ManoGestureContinuous
{
    NO_GESTURE = -1,
    HOLD_GESTURE = 1,
    OPEN_HAND_GESTURE = 2,
    OPEN_PINCH_GESTURE = 3,
    CLOSED_HAND_GESTURE = 4,
    POINTER_GESTURE = 5,
    PUSH_POINTING_GESTURE = 6
}
;

/// <summary>
///  Information about the gesture performed by the user.
/// </summary>
public struct GestureInfo
{
    /// <summary>
    /// Class or gesture family.
    /// </summary>
    public ManoClass mano_class;

    /// <summary>
    /// Continuous gestures are those that are mantained throug multiple frames.
    /// </summary>
    public ManoGestureContinuous mano_gesture_continuous;

    /// <summary>
    /// Trigger gestures are those that happen in one frame.
    /// </summary>
    public ManoGestureTrigger mano_gesture_trigger;

    /// <summary>
    /// State is the information of the pose of the hand depending on each class
    /// The values go from 0 (most open) to 13 (most closed)
    /// </summary>
    public int state;

    /// <summary>
    /// Represents which side of the hand is seen by the camera.
    /// </summary>
    public HandSide hand_side;
}