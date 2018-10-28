using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundModeButtonSelector : MonoBehaviour
{


    [SerializeField]
    Image currentModeImage;

    public BackgroundMode myBackgroundMode;
    private Color thisModeColor;

    void Start()
    {
        thisModeColor = GetComponent<Image>().color;
    }

    /// <summary>
    /// Set the appropriate background mode for the Manomotion Manager to detect the hand
    /// </summary>
    public void SelectBackgroundMode()
    {
        ManomotionManager.Instance.SetBackgroundMode(myBackgroundMode);
        currentModeImage.color = thisModeColor;
    }
}
