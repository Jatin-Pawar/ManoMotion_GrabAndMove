using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGizmos : MonoBehaviour {


    GizmoManager _gizmoManager;

    private void Start()
    {
        _gizmoManager = GetComponent<GizmoManager>();
    }

    /// <summary>
    /// Toggles the boolean value of showing the rotation
    /// </summary>
    public void ToggleShowRotation()
    {
        _gizmoManager.Show_rotation = !_gizmoManager.Show_rotation;
    }

    /// <summary>
    /// Toggles the boolean value of showing the depth
    /// </summary>
    public void ToggleShowDepth()
    {
        _gizmoManager.Show_depth = !_gizmoManager.Show_depth;
    }

    /// <summary>
    /// Toggles the boolean value of showing the hand states
    /// </summary>
    public void ToggleShowHandStates()
    {
        _gizmoManager.Show_hand_states = !_gizmoManager.Show_hand_states;
    }

    /// <summary>
    /// Toggles the boolean value of showing the manoclass
    /// </summary>
    public void ToggleShowManoclass()
    {
        _gizmoManager.Show_mano_class = !_gizmoManager.Show_mano_class;
    }

    /// <summary>
    /// Toggles the boolean value of showing the trigger gesture
    /// </summary>
    public void ToggleShowTriggerGesture()
    {
        _gizmoManager.Show_trigger_gesture = !_gizmoManager.Show_trigger_gesture;
    }

    /// <summary>
    /// Toggles the boolean value of showing the continuous gesture
    /// </summary>
    public void ToggleShowContinuousGesture()
    {
        _gizmoManager.Show_continuous_gesture = !_gizmoManager.Show_continuous_gesture;
    }

    /// <summary>
    /// Toggles the boolean value of showing bounding box information
    /// </summary>
    public void ToggleBoundingBox()
    {
        _gizmoManager.Show_bounding_box = !_gizmoManager.Show_bounding_box;
    }

    /// <summary>
    /// Toggles the boolean value of showing flags.
    /// </summary>
	public void ToggleShowFlags(){

		_gizmoManager.Show_flags = !_gizmoManager.Show_flags;
	}

 


}
