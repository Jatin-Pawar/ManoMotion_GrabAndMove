using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandDetectionModeButtonBehavior : MonoBehaviour {

	public 
	Color transparentWhite = Color.white;
	void Start()
	{
		transparentWhite.a = 0.2f;
	}


	[SerializeField]
	GameObject[] handButtons;

	public SelectedHand _handModeID;

	public bool isSelected;

	/// <summary>
	/// Sets the Manomotion Manager to detect for right or left hand and highlights the appropriate icon
	/// </summary>
	public void SelectHandDetectionMode()
	{

        ManomotionManager.Instance.SetSelectedHand(_handModeID);
        for (int i = 0; i < handButtons.Length; i++)
        {
            handButtons[i].GetComponent<Image>().color = transparentWhite;
            handButtons[i].GetComponent<HandDetectionModeButtonBehavior>().isSelected = false;
        }

        this.GetComponent<Image>().color = Color.white;

	}
}
