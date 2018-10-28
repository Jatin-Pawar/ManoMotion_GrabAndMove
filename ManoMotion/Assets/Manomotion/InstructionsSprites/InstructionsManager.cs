using UnityEngine.UI;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    private static InstructionsManager _instance;
    public static InstructionsManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    [SerializeField]
    GameObject[] componentsToStart;

    [SerializeField]
    GameObject[] slides;

    [SerializeField]
    Text currentStateText;

    [SerializeField]
    GameObject startButton;
    Color32 transparentWhite = new Color32(255, 255, 255, 50);
    [SerializeField]
    Image[] circles;
    [SerializeField]
    GameObject[] visualComponents;

    public enum InstructionState
    {
        BackgroundModeSelection,
        Calibration,
        Finish
    }
    public InstructionState currentInstractionState;


    public void IncreaseSlide()
    {
        if ((int)currentInstractionState < slides.Length - 1)
        {
            currentInstractionState = (InstructionState)((int)currentInstractionState + 1);
            ShowSlide((int)currentInstractionState);
        }
    }
    public void DecreaseSlide()
    {
        if ((int)currentInstractionState > 0)
        {
            currentInstractionState = (InstructionState)((int)currentInstractionState - 1);
            ShowSlide((int)currentInstractionState);
        }
    }


    public void ShowSlide(int value)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }
        slides[value].SetActive(true);
        UpdateVisualComponents();
    }

    public void StartInstructions()
    {
        GetPreviousManoVisualizationValues();
        if (PlayerPrefs.GetInt("hasSeenInstructions") == 0)
        {
            Debug.Log("has seen is 0");
            DisplayVisualComponents(true);
            HideManomotionComponents();
            currentInstractionState = InstructionState.BackgroundModeSelection;
            ShowSlide((int)currentInstractionState);
            PlayerPrefs.SetInt("hasSeenInstructions", 1);
            //  GetPreviousManoVisualizationValues();
            //  TurnOffManovizualizationValues();
        }
        else
        {
            Debug.Log("has seen is 1");
            StopShowingInstructions();
            ShowManomotionComponents();
        }
    }

    public void ForceInstructions()
    {
        DisplayVisualComponents(true);
        HideManomotionComponents();
        currentInstractionState = InstructionState.BackgroundModeSelection;
        ShowSlide((int)currentInstractionState);
        PlayerPrefs.SetInt("hasSeenInstructions", 1);
        GetPreviousManoVisualizationValues();
        //  TurnOffManovizualizationValues();
    }

    private void UpdateVisualComponents()
    {
        switch (currentInstractionState)
        {

            case InstructionState.BackgroundModeSelection:
                currentStateText.text = "Background Selection";
                startButton.SetActive(false);
                SetCircleVisibility(true);
                break;
            case InstructionState.Calibration:
                currentStateText.text = "How to Calibrate";
                startButton.SetActive(false);
                SetCircleVisibility(true);
                break;
            case InstructionState.Finish:
                currentStateText.text = "";
                startButton.SetActive(true);
                SetCircleVisibility(false);
                break;
            default:
                break;
        }
        HighlightCircles((int)currentInstractionState);
    }

    private void HighlightCircles(int value)
    {
        for (int i = 1; i < circles.Length; i++)
        {
            if (i <= value)
            {
                circles[i].color = Color.white;
            }
            else
            {
                circles[i].color = transparentWhite;
            }
        }
    }

    void SetCircleVisibility(bool value)
    {
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].gameObject.SetActive(value);
        }
    }

    void ShowManomotionComponents()
    {

        for (int i = 0; i < componentsToStart.Length; i++)
        {
            componentsToStart[i].SetActive(true);
        }

    }

    void HideManomotionComponents()
    {
        for (int i = 0; i < componentsToStart.Length; i++)
        {
            componentsToStart[i].SetActive(false);
        }
    }

    public void StopShowingInstructions()
    {
        this.gameObject.SetActive(false);
        ShowManomotionComponents();
        DisplayVisualComponents(false);
        // ReInstateVizualizationValues();
    }

    private void DisplayVisualComponents(bool value)

    {
        for (int i = 0; i < visualComponents.Length; i++)
        {
            visualComponents[i].SetActive(value);
        }
    }

    [SerializeField]
    ManoVisualization manoVisualization;

    public bool showBoundingBox = false, showContour, showInner, showFingerTips, showFingerTipLabels, showPalmCenter, showHandLayer, showBackgroundLayer = true;

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
    }

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

    }

}
