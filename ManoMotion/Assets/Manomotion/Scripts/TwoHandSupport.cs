using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandSupport : MonoBehaviour {

	// Use this for initialization
	void Update () {
		//Debug.Log ("ManomotionManager.Instance.Manomotion_Session.two_hands_supported " + ManomotionManager.Instance.Manomotion_Session.two_hands_supported);
        this.gameObject.SetActive(ManomotionManager.Instance.Manomotion_Session.two_hands_supported == 1);
    
	}

   
   
}
