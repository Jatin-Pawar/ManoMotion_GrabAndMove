using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used for visualization
/// </summary>
public struct VisualizationInfo
{
    /// <summary>
    /// Output binary with all the hands  found
    /// </summary>
    public Texture2D binary_image;

    /// <summary>
    /// Input image that will be processed
    /// </summary>
    public Texture2D rgb_image; 
}