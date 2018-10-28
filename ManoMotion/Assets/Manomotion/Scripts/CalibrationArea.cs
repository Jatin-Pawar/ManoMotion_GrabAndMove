using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CalibrationArea : MonoBehaviour {

  public  void OnMouseOver()
    {
        ManoCalibration.Instance.inReach = true;
    
    }

    public void OnMouseExit()
    {
        ManoCalibration.Instance.inReach = false;

    }

}
