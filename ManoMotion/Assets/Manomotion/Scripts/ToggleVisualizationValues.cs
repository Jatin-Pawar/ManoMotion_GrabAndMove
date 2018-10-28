using UnityEngine;

public class ToggleVisualizationValues : MonoBehaviour
{
	


	ManoVisualization _manoVisualization;

    private void Start()
    {
        _manoVisualization = GetComponent<ManoVisualization>();
    }

    public void ToggleShowInner()
	{
		_manoVisualization.Show_inner = !_manoVisualization.Show_inner;
	}

    /// <summary>
    /// Toggles the boolean value for showing the contour particles.
    /// </summary>
	public void ToggleShowContour()
	{
		_manoVisualization.Show_contour = !_manoVisualization.Show_contour;
	}

    /// <summary>
    /// Toggles the boolean value for showing the palm center particle.
    /// </summary>
	public void ToggleShowPalmCenter()
	{
		_manoVisualization.Show_palm_center = !_manoVisualization.Show_palm_center;
	}

    /// <summary>
    /// Toggles the boolean value for showing the fingertip particles.
    /// </summary>
	public void ToggleShowFingertips()
	{
		_manoVisualization.Show_fingertips = !_manoVisualization.Show_fingertips;
	}

    /// <summary>
    /// Toggles the boolean value for showing the fingertip labels.
    /// </summary>
	public void ToggleShowFingertipLabels()
	{
		_manoVisualization.Show_fingertip_labels = !_manoVisualization.Show_fingertip_labels;
	}

    /// <summary>
    /// Toggles the boolean value for showing hand layer.
    /// </summary>
	public void ToggleShowHandLayer()
	{
		_manoVisualization.Show_hand_layer = !_manoVisualization.Show_hand_layer;
	}

    /// <summary>
    /// Toggles the boolean of showing the background layer.
    /// </summary>
	public void ToggleShowBackgroundLayer()
	{
		_manoVisualization.Show_background_layer = !_manoVisualization.Show_background_layer;
	}

    /// <summary>
    /// Toggles the boolean value for showing the bounding box.
    /// </summary>
	public void ToggleBoundingBox()
	{
		_manoVisualization.Show_bounding_box = !_manoVisualization.Show_bounding_box;
	}
}
