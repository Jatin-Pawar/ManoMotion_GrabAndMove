using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[AddComponentMenu("ManoMotion/ManoEvents")]
public class ManoEvents : MonoBehaviour
{
    #region Singleton
    private static ManoEvents _instance;
    public static ManoEvents Instance
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
    #endregion

    public delegate void ManoEvent();
    public delegate void ManoEvent<T>(T item);
    public delegate void ManoEvent<T1, T2>(T1 item1, T2 item2);
    public static event ManoEvent OnCalibrationSuccess, OnCalibrationFailed;



    [SerializeField]
    Animator debugLogAnimator;
    string debugMessage = "";


    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("More than 1 Mano events instances in scene");
        }
    }

    void Update()
    {

        HandleManomotionMessages();

    }

    /// <summary>
    /// Interprets the message from the Manomotion Manager class and assigns a string message to be displayed.
    /// </summary>
    void HandleManomotionMessages()
    {
        switch (ManomotionManager.Instance.Manomotion_Session.flag)
        {
            case Flags.FLAG_CALIBRATING:
                break;
            case Flags.FLAG_CALIBRATION_SUCCESS:
                DisplayLogMessage("calibration success");
                break;
            case Flags.FLAG_CALIBRATION_FAIL:
                DisplayLogMessage("calibration failed");
                break;
            case Flags.FLAG_LICENSE_OK:
                break;
            case Flags.FLAG_LICENSE_KEY_NOT_FOUND:
                DisplayLogMessage("Licence key not found");
                break;
            case Flags.FLAG_LICENSE_EXPIRED_WARNING:
                DisplayLogMessage("Licence expired");
                break;
            case Flags.FLAG_LICENSE_INVALID_PLAN:
                DisplayLogMessage("Invalid plan");
                break;
            case Flags.FLAG_LICENSE_KEY_BLOCKED:
                DisplayLogMessage("Licence key blocked");
                break;
            case Flags.FLAG_INVALID_ACCESS_TOKEN:
                DisplayLogMessage("Invalid access token");
                break;
            case Flags.FLAG_LICENSE_ACCESS_DENIED:
                DisplayLogMessage("Licence access denied");
                break;
            case Flags.FLAG_LICENSE_MAX_NUM_DEVICES:
                DisplayLogMessage("Licence key blocked");
                break;
            case Flags.FLAG_UNKNOWN_SERVER_REPLY:
                DisplayLogMessage("Unknown Server Reply");
                break;
            case Flags.FLAG_LICENSE_PRODUCT_NOT_FOUND:
                DisplayLogMessage("Licence product");
                break;
            case Flags.FLAG_LICENSE_INCORRECT_INPUT_PARAMETER:
                DisplayLogMessage("Incorect Licence");
                break;
            case Flags.FLAG_LICENSE_INTERNET_REQUIRED:
                DisplayLogMessage("Internet Required");
                break;
            case Flags.FLAG_BOUNDLE_ID_DOESENT_MATCH:
                DisplayLogMessage("Bundle ID does not match");
                break;
            case Flags.TWO_HANDS_REQUESTED_BUT_NOT_ALLOWED:
                DisplayLogMessage("Licence requires 2 hand support");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Displays Log messages from the Manomotion Flags 
    /// </summary>
    /// <param name="message">Requires the string message to be displayed</param>
    void DisplayLogMessage(string message)
    {

        debugLogAnimator.Play("SlideIn");
        debugLogAnimator.GetComponentInChildren<Text>().text = message;
    }

}
