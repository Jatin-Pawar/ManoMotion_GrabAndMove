using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

[AddComponentMenu("ManoMotion/ManoCalibration")]
public class ManoCalibration : MonoBehaviour
{
    #region Singleton
    private static ManoCalibration _instance;
    public static ManoCalibration Instance
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

    enum CalibrationTrigger
    {
        ONE_FINGER = 1,
        TWO_FINGERS = 2,
        THREE_FINGERS = 3

    };

    enum CalibrationTime
    {
        ON_TOUCH,
        ON_RELEASE,
        ON_HOLD
    };


    [Tooltip("Select when should the calibration occur")]
    [SerializeField]
    CalibrationTime calibration_time;

    [Tooltip("Select which event starts a calibration")]
    [SerializeField]
    CalibrationTrigger calibration_trigger;

    [Tooltip("Delay for the calibration to start")]
    [SerializeField]
    float calibration_delay = 0.2f;

    int needed_fingers = 1000;
    float time_touching = 0;
    const float HOLD_TIME = .5f;
    bool canCalibrate = true;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }else{
            Destroy(this.gameObject);
            Debug.Log("More than 1 ManoCalibration instances");
        }
        needed_fingers = (int)calibration_trigger;
        LoadCalibration();

    }

   
    void Update()
    {
        DetectCalibration();
        SaveCalibration();
    }

    /// <summary>
    /// Stores the calibration value so it can be re used.
    /// </summary>
    void SaveCalibration()
    {
        if (ManomotionManager.Instance.Manomotion_Session.flag == Flags.FLAG_CALIBRATION_SUCCESS)
        {
            PlayerPrefs.SetInt("calibration_value", ManomotionManager.Instance.Manomotion_Session.calibration_value);
        }
    }

    /// <summary>
    /// Loads the stored calibration value.
    /// </summary>
    void LoadCalibration()
    {
        ManomotionManager manager = ManomotionManager.Instance;
        manager.SetCalibration(PlayerPrefs.GetInt("calibration_value", 15));
    }




    public bool inReach;
    bool locker = false;

 

    /// <summary>
    /// Initiates a calibration according to the fingertip interaction with the screen.
    /// The interaction only occurs within the interaction area of the screen
    /// </summary>
    void DetectCalibration()
    {
        if (Input.touchCount == needed_fingers)
        {



            Touch[] my_touches = Input.touches;
            switch (my_touches[0].phase)
            {
                case TouchPhase.Began:
                    time_touching += Time.deltaTime;
                    break;
                case TouchPhase.Moved:
                    time_touching += Time.deltaTime;
                    break;
                case TouchPhase.Stationary:
                    time_touching += Time.deltaTime;
                    break;
                case TouchPhase.Ended:
                    time_touching = -1;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }


        }
        else
        {
            canCalibrate = true;
            time_touching = 0;
        }
        switch (calibration_time)
        {
            case CalibrationTime.ON_TOUCH:
                if (time_touching > 0)
                    StartCoroutine(Calibrate(calibration_delay));
                break;
            case CalibrationTime.ON_RELEASE:
                if (time_touching < 0)
                    StartCoroutine(Calibrate(calibration_delay));
                break;
            case CalibrationTime.ON_HOLD:
                if (time_touching > HOLD_TIME)
                    StartCoroutine(Calibrate(calibration_delay));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Calibrate the Manomotion Manager after a given delay.
    /// </summary>
    /// <returns>The calibrate.</returns>
    /// <param name="delay">Requires a float value of the delay before the calibration will be executed.</param>
    IEnumerator Calibrate(float delay)
    {

        if (!locker && canCalibrate && inReach)
        {
            locker = true;

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();

#endif
            yield return new WaitForSeconds(delay);
            ManomotionManager.Instance.Calibrate();
            locker = false;
            canCalibrate = false;
            Debug.Log("Calibration coroutine");
        }
        yield return new WaitForSeconds(0);


    }

}
