using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public enum Warning
{
    NO_WARNING = 0,
    WARNING_HAND_NOT_FOUND = 1,
    WARNING_CANT_DIFFERENTIATE_BACKGROUND = 2,
    WARNING_APPROACHING_LOWER_EDGE = 3,
    WARNING_APPROACHING_UPPER_EDGE = 4,
    WARNING_APPROACHING_LEFT_EDGE = 5,
    WARNING_APPROACHING_RIGHT_EDGE = 6,
    WARNING_HAND_TOO_CLOSE = 7,
    FLAG_NOISE = 8,

};
public enum Hand
{
    LEFT =0,
    RIGHT=1,
    UNKNOWN
};
/// <summary>
/// Contrains information about the hand
/// </summary>
public struct HandInfo {

  
    /// <summary>
    /// Information about position
    /// </summary>
    public TrackingInfo tracking_info;

    /// <summary>
    /// Information about gesture
    /// </summary>
    public GestureInfo gesture_info;

    /// <summary>
    /// Warnings of conditions that could mean problems on detection
    /// </summary>
    public Warning warning;

    /// <summary>
    /// Information about if the hand is right or left
    /// </summary>
    public Hand hand;

}