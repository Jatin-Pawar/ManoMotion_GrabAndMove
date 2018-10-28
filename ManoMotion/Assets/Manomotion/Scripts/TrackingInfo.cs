using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using System.Runtime.InteropServices;

/// <summary>
/// index of this points on the fingertip array
/// </summary>
public enum FingerTipIndex
{
    THUMB_INDEX=4,
    POINTER_INDEX=3,
    MIDDLE_INDEX=2,
    RING_INDEX=1,
    PINKY_INDEX=0
}

/// <summary>
/// Contains information about position and tracking of the hand
/// </summary> 
[StructLayout(LayoutKind.Sequential)]
public struct TrackingInfo
{
    /// <summary>
    /// Box that encloses the hand
    /// normalized values between 0..1
    /// </summary>
    public BoundingBox bounding_box;

    /// <summary>
    /// Center of the hand
    /// normalized values between 0..1
    /// </summary>
    public Vector3 palm_center;

    /// <summary>
    /// Rotation of the hand
    /// normalized values between 0..1
    /// </summary>
    public float rotation;

    /// <summary>
    /// Estimated value for depth of the hand
    /// normalized values between 0..1
    /// </summary>
    public float relative_depth;

    /// <summary>
    /// Amount of contour points in this frame, used to know how many of the values in the array are valid
    /// </summary>
    public int amount_of_contour_points;

    /// <summary>
    /// Amount of inner points in this frame, used to know how many of the values in the array are valid
    /// </summary>
    public int amount_of_inner_points;

    /// <summary>
    /// Position of the fingertips, to get a specific fingertip use @FingerTipIndex enum
    /// normalized values between 0..1
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public Vector3[] finger_tips;

    /// <summary>
    /// Position of points on the contour of the hand
    /// normalized values between 0..1
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
    public Vector3[] contour_points;

    /// <summary>
    /// Position of points inside the hand, 
    /// 
    /// normalized values between 0..1
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
    public Vector3[] inner_points;
}