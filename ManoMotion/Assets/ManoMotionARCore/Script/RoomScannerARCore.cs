using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public class RoomScannerARCore : MonoBehaviour
{
    [SerializeField]
    ManoVisualization manoVisualization;
    [SerializeField]
    GameObject scaningStatusImage;
    [SerializeField]
    GameObject GizmoCanvas;
    [SerializeField]
    GameObject ManoMotionUI;
    [SerializeField]
    ChooseBackgroundBehavior chooseBackgrounds;

    bool previousValuesStored = false;
    bool previousValuesReinstated = false;

    private void Update()
    {
        DisplayVisualsBasedOnScanning();
    }
    private List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();
    bool hasFoundAPlane;
    // <summary>
    /// Makes use of the number of planes detected in order to guide the user into scanning the room for planes. Meanwhile all visualization prefabs and attributes are turned off.
    void DisplayVisualsBasedOnScanning()
    {


        GoogleARCore.Session.GetTrackables<DetectedPlane>(detectedPlanes);


        hasFoundAPlane = detectedPlanes.Count > 0;


        if (hasFoundAPlane)
        {
            if (!previousValuesReinstated)
            {
                ReInstateVizualizationValues();
                StartCoroutine(chooseBackgrounds.CloseAvailableBackgroundMenuAfter());
            }
        }
        else
        {
            if (!previousValuesStored)
            {
                GetPreviousManoVisualizationValues();
                TurnOffManovizualizationValues();
            }
        }


        scaningStatusImage.SetActive(!hasFoundAPlane);
        GizmoCanvas.SetActive(hasFoundAPlane);
        ManoMotionUI.SetActive(hasFoundAPlane);
    }
    public bool showBoundingBox = false, showContour, showInner, showFingerTips, showFingerTipLabels, showPalmCenter, showHandLayer, showBackgroundLayer = true;

    /// <summary>
    /// Stores the current bool values of ManoVisualization so they can be later on used to reinstate the visualization state
    /// </summary>
    void GetPreviousManoVisualizationValues()
    {
        showBoundingBox = manoVisualization.Show_bounding_box;
        showContour = manoVisualization.Show_contour;
        showInner = manoVisualization.Show_inner;
        showFingerTips = manoVisualization.Show_fingertips;
        showFingerTipLabels = manoVisualization.Show_fingertip_labels;
        showPalmCenter = manoVisualization.Show_palm_center;
        showHandLayer = manoVisualization.Show_hand_layer;
        showBackgroundLayer = manoVisualization.Show_background_layer;
        previousValuesStored = true;
    }

    /// <summary>
    /// Turns off the Manovisualuzation booleans so they wont appear in the user interface
    /// </summary>
    void TurnOffManovizualizationValues()
    {
        manoVisualization.Show_bounding_box = false;
        manoVisualization.Show_contour = false;
        manoVisualization.Show_inner = false;
        manoVisualization.Show_fingertips = false;
        manoVisualization.Show_fingertip_labels = false;
        manoVisualization.Show_palm_center = false;
        manoVisualization.Show_hand_layer = false;
        manoVisualization.Show_bounding_box = false;
    }

    /// <summary>
    /// Makes use of the previously stored values from ManoVisualization and re instates them to the previous bool value
    /// </summary>
    void ReInstateVizualizationValues()
    {
        manoVisualization.Show_bounding_box = showBoundingBox;
        manoVisualization.Show_contour = showContour;
        manoVisualization.Show_inner = showInner;
        manoVisualization.Show_fingertips = showInner;
        manoVisualization.Show_fingertip_labels = showFingerTipLabels;
        manoVisualization.Show_palm_center = showPalmCenter;
        manoVisualization.Show_hand_layer = showHandLayer;
        manoVisualization.Show_background_layer = showBackgroundLayer;
        previousValuesReinstated = true;
    }

}
