using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {

    private static ApplicationManager _instance;

    public static ApplicationManager Instance
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
        if (_instance==null)
        {
            _instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }
    void Start () {
        InstructionsManager.Instance.StartInstructions();
	}
	
   
    public void DisplayInstructions(){
        InstructionsManager.Instance.gameObject.SetActive(true);
        InstructionsManager.Instance.ForceInstructions();
    }

    public void StartMainApplication(){
        
        InstructionsManager.Instance.StopShowingInstructions();
    }


}
