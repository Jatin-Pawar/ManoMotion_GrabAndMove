using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InteractionArea : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;
    //This stores the GameObject’s original color
    Color m_OriginalColor;

    Image interactionAreaImage;
    // Use this for initialization
    void Start()
    {
        interactionAreaImage = GetComponent<Image>();
        interactionAreaImage.color = Color.green;
    }



    public void EnterMouse()
    {
        interactionAreaImage.color = Color.green;
    }
    public void ExitMouse()
    {
        interactionAreaImage.color = Color.red;
    }
}
