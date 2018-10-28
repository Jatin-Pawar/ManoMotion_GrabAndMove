using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Box that encloses the hand
/// normalized values between 0..1
/// </summary>
public struct BoundingBox
{
    /// <summary>
    /// Point at the top left to start the enclosing box
    /// normalized values between 0..1
    /// </summary>
    public Vector3 top_left;
    /// <summary>
    /// width of the enclosing box
    /// normalized values between 0..1
    /// </summary>
    public float width;
    /// <summary>
    /// height of the enclosing box
    /// normalized values between 0..1
    /// </summary>
    public float height;
}