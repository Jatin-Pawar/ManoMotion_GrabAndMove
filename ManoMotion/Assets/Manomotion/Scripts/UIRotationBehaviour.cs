using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotationBehaviour : MonoBehaviour {

	public delegate void MenuEnabled();
	public static event MenuEnabled OnMenuEnabled;


	void OnEnable()
	{
        Debug.Log("OnMenuEnabled has been used");

        if (OnMenuEnabled != null)
			OnMenuEnabled ();
		else {
			Debug.LogError ("OnMenuEnabled had nothing suscribed");
		}
	}

}
