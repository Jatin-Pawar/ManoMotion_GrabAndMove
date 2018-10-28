using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearOnEnable : MonoBehaviour
{

    [SerializeField]
    Vector3 positionToAppear;
    // Use this for initialization

    /// <summary>
    /// When the Layering is enabled the sphere will be moved on the positionToAppear vector position
    /// </summary>
    void OnEnable()
    {
        transform.position = Camera.main.ViewportToWorldPoint(positionToAppear);
    }


}
