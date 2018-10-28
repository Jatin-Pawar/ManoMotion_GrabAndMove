using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconBehavior : MonoBehaviour {

    [SerializeField]
    Sprite activeFrame, inactiveFrame;

    public bool isActive;

    private Button thisButton;
    private Image buttonFrame,buttonIcon;

    private Color activeColor;
    void Start () {
        activeColor = new Color32(61, 87, 127, 255);
        thisButton = this.GetComponent<Button>();
        buttonFrame = transform.Find("Frame").GetComponent<Image>();
        buttonIcon = transform.Find("Icon").GetComponent<Image>();

        UpdateIconAndFrame(isActive);
    }

    /// <summary>
    /// Updates the icon according to its state (on/off)
    /// </summary>
    /// <param name="state">Requires the state of the icon</param>
    private void UpdateIconAndFrame(bool state)
    {
        if (state)
        {
            buttonFrame.sprite = activeFrame;
            buttonIcon.color =  activeColor;
        }
        else
        {
            buttonFrame.sprite = inactiveFrame;
            buttonIcon.color = Color.white;
        }
    }

    /// <summary>
    /// Toggles the state of the icon.
    /// </summary>
    public void ToggleActive()
    {
        isActive = !isActive;
        UpdateIconAndFrame(isActive);
    }

    public void DeactivateIcon(){
        isActive = false;
        UpdateIconAndFrame(isActive);
    }
}
