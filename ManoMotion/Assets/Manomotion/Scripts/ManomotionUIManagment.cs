using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ManomotionUIManagment : MonoBehaviour
{
    [SerializeField]
    GameObject[] handButtons;

    [SerializeField]
    Text FPSValueText, processingTimeValueText, versionText;

    private void Start()
    {
        string fullversion = ManomotionManager.Instance.Manomotion_Session.version.ToString();
        string test = fullversion[1].ToString();
        versionText.text = "Version ";
        for (int i = 0; i < fullversion.Length; i++)
        {
            versionText.text += fullversion[i].ToString() + ".";

        }

        versionText.text += " Beta";
    }

    void Update()
    {

        UpdateFPSText();
        UpdateProcessingTime();
    }
    /// <summary>
    /// Toggles the visibility of a Gameobject.
    /// </summary>
    /// <param name="givenObject">Requires a Gameobject.</param>
	public void ToggleUIElement(GameObject givenObject)
    {
        givenObject.SetActive(!givenObject.activeInHierarchy);
    }

    /// <summary>
    /// Updates the text field with the calculated Frames Per Second value.
    /// </summary>
	public void UpdateFPSText()
    {
        FPSValueText.text = ManomotionManager.Instance.Fps.ToString();
    }

    /// <summary>
    /// Updates the text field with the calculated processing time value.
    /// </summary>
	public void UpdateProcessingTime()
    {
        processingTimeValueText.text = ManomotionManager.Instance.Processing_time.ToString() + " ms";
    }

    /// <summary>
    /// Hides the hand buttons.
    /// </summary>
    public void HideHandButtons()
    {

        for (int i = 0; i < handButtons.Length; i++)
        {
            handButtons[i].SetActive(false);
        }
    }

    /// <summary>
    /// Shows the hand buttons.
    /// </summary>
    public void ShowHandButtons()
    {
        for (int i = 0; i < handButtons.Length; i++)
        {
            handButtons[i].SetActive(true);
        }

    }
}
