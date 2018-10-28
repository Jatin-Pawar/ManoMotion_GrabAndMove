using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

[AddComponentMenu("ManoMotion/ManoMotion Manager")]
[RequireComponent(typeof(ManoCalibration))]
[RequireComponent(typeof(ManoEvents))]
public class ManomotionManager : MonoBehaviour
{
    #region Singleton
    protected static ManomotionManager instance;
    #endregion

    #region consts


    internal const int STARTING_WIDTH = 320;
    internal const int STARTING_HEIGHT = 240;
    const int FINGERTIPS_MAX_AMOUNT = 5;
    const int CONTOUR_POINTS_MAX_AMOUNT = 200;
    #endregion

    #region variables
    protected HandInfoUnity[] hand_infos;
    protected VisualizationInfo visualization_info;
    protected Session manomotion_Session;

    protected int _frame_number;
    protected int _width = STARTING_WIDTH;
    protected int _height = STARTING_HEIGHT;
    protected int _fps;
    protected int _processing_time;
    protected int _amount_of_contour_points;
    protected int _amount_of_inner_points;
    protected bool _initialized = false;

    protected WebCamTexture _mCamera = null;
    private float fpsCooldown = 0;
    private int frameCount = 0;
    private List<int> processing_time_list = new List<int>();


    protected Color32[] _pixels, _MRPixels0, _MRPixels1, _binary_frame_320;

    [Tooltip("Insert the key gotten from the webpage here https://www.manomotion.com/my-account/licenses/")]
    [SerializeField]
    private string serial_key;

    public string Serial_key
    {
        get
        {
            return serial_key;
        }

        set
        {
            serial_key = value;
        }
    }

    #endregion

    #region imports

#if UNITY_IOS
    const string library = "__Internal";
#elif UNITY_ANDROID
	const string library = "manomotion";
#else
	const string library = "manomotion";
#endif
    [DllImport(library)]
    private static extern void init(string serial_key);

    [DllImport(library)]
    private static extern void processFrame(ref HandInfo hand_info0, ref HandInfo hand_info1, ref Session manager_info);

    [DllImport(library)]
    private static extern void calibrate();

    [DllImport(library)]
    private static extern void setFrameArray(Color32[] frame);

    [DllImport(library)]
    private static extern void setMRFrameArray(Color32[] frame0, Color32[] frame1);

    [DllImport(library)]
    private static extern void setResolution(int width, int height);

    [DllImport(library)]
    private static extern void setImageBinary(Color32[] frame);

    #endregion

    #region Propperties
    internal int Processing_time
    {
        get
        {
            return _processing_time;
        }
    }

    internal int Fps
    {
        get
        {
            return _fps;
        }
    }

    internal int Height
    {
        get
        {
            return _height;
        }
    }

    internal int Width
    {
        get
        {
            return _width;
        }
    }

    internal int Frame_number
    {
        get
        {
            return _frame_number;
        }
    }

    internal VisualizationInfo Visualization_info
    {
        get
        {
            return visualization_info;
        }
    }

    internal HandInfoUnity[] Hand_infos
    {
        get
        {
            return hand_infos;
        }
    }

    public Session Manomotion_Session
    {
        get
        {
            return manomotion_Session;
        }
        set
        {
            manomotion_Session = value;
        }
    }

    public static ManomotionManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void SetSelectedHand(SelectedHand value)
    {
        manomotion_Session.hand_selection = value;
    }
    protected bool hasReceivedAFrame;

    #endregion

    #region init_methods

    protected void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("More than 1 Manomotionmanager in scene");
        }

    }

    protected void Start()
    {
        StartWebCamTexture();
        InstantiateSession();

        PickResolution(STARTING_WIDTH, STARTING_HEIGHT);
        SetUnityConditions();
        _initialized = true;
    }

    /// <summary>
    /// Sets the calibration value in order to be used in the future.
    /// </summary>
    /// <param name="value">Requires the int value of calibration.</param>
    internal void SetCalibration(int value)
    {
        manomotion_Session.calibration_value = value;
    }

    /// <summary>
    /// Starts the camera for input.
    /// </summary>
    protected void StartWebCamTexture()
    {
        _mCamera = new WebCamTexture(WebCamTexture.devices[0].name, _width, _height);
        _mCamera.requestedFPS = 120;
        _mCamera.Play();
    }

    /// <summary>
    /// Picks the resolution.
    /// </summary>
    /// <param name="width">Requires a width value.</param>
    /// <param name="height">Requires a height value.</param>
    protected void PickResolution(int width, int height)
    {
        Debug.Log("changed from (" + _width + "," + _height + ") to (" + width + "," + height + ")");
        _width = width;
        _height = height;
        InstantiateHandInfos();
        InstantiateVisualisationInfo();
        InitiateTextures();
        InitiateLibrary();
    }

    /// <summary>
    /// Instantiates the manager info.
    /// </summary>
    protected void InstantiateSession()
    {
        manomotion_Session = new Session();
        manomotion_Session.background_mode = BackgroundMode.BACKGROUND_NORMAL;
        manomotion_Session.add_on = AddOn.DEFAULT;
#if UNITY_ANDROID
	manomotion_Session.current_plataform = Platform.UNITY_ANDROID;
#elif UNITY_IOS
        manomotion_Session.current_plataform = Platform.UNITY_IOS;
#endif
        manomotion_Session.flag = 0;
        manomotion_Session.image_format = ImageFormat.RGBA_IMAGE;
        manomotion_Session.orientation = SupportedOrientation.LANDSCAPE_LEFT;
        manomotion_Session.is_two_hands_enabled_by_developer = 0;
    }

    /// <summary>
    /// Initializes the values for the hand information.
    /// </summary>
    private void InstantiateHandInfos()
    {
        //delete me
        hand_infos = new HandInfoUnity[2];
        for (int i = 0; i < hand_infos.Length; i++)
        {
            hand_infos[i].hand_info = new HandInfo();
            hand_infos[i].hand_info.hand = 0;
            hand_infos[i].hand_info.warning = Warning.NO_WARNING;
            hand_infos[i].hand_info.gesture_info = new GestureInfo();
            hand_infos[i].hand_info.gesture_info.hand_side = 0;
            hand_infos[i].hand_info.gesture_info.mano_class = ManoClass.NO_HAND;
            hand_infos[i].hand_info.gesture_info.mano_gesture_continuous = ManoGestureContinuous.NO_GESTURE;
            hand_infos[i].hand_info.gesture_info.mano_gesture_trigger = ManoGestureTrigger.NO_GESTURE;
            hand_infos[i].hand_info.gesture_info.state = 0;
            hand_infos[i].cut_rgb = new Texture2D(_width, _height);

            hand_infos[i].hand_info.tracking_info = new TrackingInfo();
            hand_infos[i].hand_info.tracking_info.bounding_box = new BoundingBox();
            hand_infos[i].hand_info.tracking_info.bounding_box.top_left = Vector3.zero;
            hand_infos[i].hand_info.tracking_info.bounding_box.height = 0;
            hand_infos[i].hand_info.tracking_info.bounding_box.width = 0;
            hand_infos[i].hand_info.tracking_info.finger_tips = new Vector3[FINGERTIPS_MAX_AMOUNT];
            hand_infos[i].hand_info.tracking_info.inner_points = new Vector3[CONTOUR_POINTS_MAX_AMOUNT];
            hand_infos[i].hand_info.tracking_info.contour_points = new Vector3[CONTOUR_POINTS_MAX_AMOUNT];

        }
    }

    /// <summary>
    /// Instantiates the visualisation info.
    /// </summary>
    private void InstantiateVisualisationInfo()
    {
        visualization_info = new VisualizationInfo();
        visualization_info.binary_image = new Texture2D(_width, _height);
        visualization_info.rgb_image = new Texture2D(_width, _height);
    }

    /// <summary>
    /// Initiates the textures.
    /// </summary>
    protected void InitiateTextures()
    {
        visualization_info.binary_image = new Texture2D(_width, _height);
        for (int i = 0; i < hand_infos.Length; i++)
        {
            hand_infos[i].cut_rgb = new Texture2D(_width, _height);
        }
        visualization_info.rgb_image = new Texture2D(_width, _height);
        visualization_info.rgb_image.Apply();
    }

    /// <summary>
    /// Initiates the library.
    /// </summary>
    protected void InitiateLibrary()
    {
        Debug.Log("Initiating ManoMotion SDK with serial key " + serial_key + " bundle id :" + Application.identifier);

        Init(serial_key);
        Debug.Log("Initiated ManoMotion SDK");
        SetVariables();
    }

    /// <summary>
    /// Sets the variables.
    /// </summary>
    private void SetVariables()
    {
        SetManoManagerVariables();
        SetVisualizationInfo();
        SetHandInfo();
    }

    /// <summary>
    /// Initializes the color information for the hands.
    /// </summary>
    private void SetHandInfo()
    {
        _MRPixels0 = new Color32[_width * _height];
        _MRPixels1 = new Color32[_width * _height];
        SetMRFrameArray();
    }

    /// <summary>
    /// Initialize the visualization info variables.
    /// </summary>
    private void SetVisualizationInfo()
    {
        _pixels = new Color32[_width * _height];
        _binary_frame_320 = new Color32[_width * _height];

        SetImageBinaries();
        SetFrameArray(_pixels);
    }

    /// <summary>
    /// Sets the initial detection to be for a right hand. Also sets the resolution for the captured image.
    /// </summary>
    private void SetManoManagerVariables()
    {
        manomotion_Session.hand_selection = SelectedHand.RIGHT_HAND;
        SetResolution(_width, _height);
        Debug.Log("resolution set to " + _width + " X " + _height);
    }

    /// <summary>
    /// Sets the Application to not go to sleep mode as well as the requested framerate.
    /// </summary>
    protected void SetUnityConditions()
    {
        // Application.targetFrameRate = 300;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    #endregion

    #region init_wrappers

    protected void Init(string serial_key)
    {
#if !UNITY_EDITOR || UNITY_STANDALONE
	    init(serial_key);
#endif
    }


    protected void SetImageBinaries()
    {
#if !UNITY_EDITOR
	    setImageBinary(_binary_frame_320);
#endif
    }

    protected void SetResolution(int width, int height)
    {
#if !UNITY_EDITOR
	    setResolution(width, height);
#endif
    }

    protected void SetFrameArray(Color32[] pixels)
    {
#if !UNITY_EDITOR
	    setFrameArray(pixels);
#endif
    }

    protected void SetMRFrameArray()
    {
#if !UNITY_EDITOR
	    setMRFrameArray(_MRPixels0, _MRPixels1);
#endif
    }

    #endregion

    #region update_methods
    protected void Update()
    {
        if (_initialized)
        {
            if (!_mCamera.isPlaying)
            {
                _mCamera.Play();
                Debug.LogWarning("Camera was not playing");
            }
            else
            {
                UpdateOrientation();
                CalculateFPSAndProcessingTime();
                UpdatePixelValues();
                ProcessManomotion();
                UpdateTexturesWithNewInfo();
            }
        }
        else
        {
            Debug.LogWarning("ManoMotion SDK is not initialized");
        }
    }

    /// <summary>
    /// Updates the orientation information as captured from the device to the manager_info
    /// </summary>
    protected void UpdateOrientation()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Unknown:
                break;
            case DeviceOrientation.Portrait:
                manomotion_Session.orientation = (SupportedOrientation)Input.deviceOrientation;
                break;
            case DeviceOrientation.PortraitUpsideDown:
                manomotion_Session.orientation = (SupportedOrientation)Input.deviceOrientation;
                break;
            case DeviceOrientation.LandscapeLeft:
                manomotion_Session.orientation = (SupportedOrientation)Input.deviceOrientation;
                break;
            case DeviceOrientation.LandscapeRight:
                manomotion_Session.orientation = (SupportedOrientation)Input.deviceOrientation;
                break;
            case DeviceOrientation.FaceUp:
                break;
            case DeviceOrientation.FaceDown:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates the textures of background and hands with new values.
    /// </summary>
    protected void UpdateTexturesWithNewInfo()
    {
        //RGB Frame
        Color32[] rgb_frame;
        rgb_frame = _pixels;
        visualization_info.rgb_image.SetPixels32(rgb_frame);
        visualization_info.rgb_image.Apply();
        //Binary Frame
        Color32[] bin_frame;
        bin_frame = _binary_frame_320;
        visualization_info.binary_image.SetPixels32(bin_frame);
        visualization_info.binary_image.Apply();

        //MR Frame
        hand_infos[0].cut_rgb.SetPixels32(_MRPixels0);
        hand_infos[0].cut_rgb.Apply();

        hand_infos[1].cut_rgb.SetPixels32(_MRPixels1);
        hand_infos[1].cut_rgb.Apply();
    }
    /// <summary>
    /// Sets the Manomotion SDK to track for 1 or 2 hands if the licence has 2 hands support.
    /// </summary>
    /// <param name="value">Requires the int value 0 for false or 1 for true</param>
    public void Set2HandSupport(int value)
    {
        manomotion_Session.is_two_hands_enabled_by_developer = value;
    }

    /// <summary>
    /// Sets the background mode for the session.
    /// Make sure to choose the appropriate background mode for a better hand detection.
    /// </summary>
    /// <param name="newMode">Requires a background mode.</param>
    internal void SetBackgroundMode(BackgroundMode newMode)
    {
        manomotion_Session.background_mode = newMode;
    }

    /// <summary>
    /// Gets the pixel values from the camera input
    /// </summary>
    virtual protected void UpdatePixelValues()
    {
        Color32[] camera_pixels = _mCamera.GetPixels32();

        if (_mCamera.width != Width || _mCamera.height!= Height)
        {

            PickResolution(_mCamera.width, _mCamera.height);
        }
        camera_pixels.CopyTo(_pixels, 0);
    }

    /// <summary>
    /// Evaluates the input frame in order to process it.
    /// </summary>
    protected void ProcessManomotion()
    {
        if (Width * Height > 0)
        {
            if (_pixels.Length == Width * Height)
            {
                long start = System.DateTime.UtcNow.Millisecond + System.DateTime.UtcNow.Second * 1000 + System.DateTime.UtcNow.Minute * 60000;
                ProcessFrame();
                long end = System.DateTime.UtcNow.Millisecond + System.DateTime.UtcNow.Second * 1000 + System.DateTime.UtcNow.Minute * 60000;
                if (start < end)
                    processing_time_list.Add((int)(end - start));

            }
            else
            {
                Debug.LogWarning("camera size doesent match: " + _pixels.Length + " != " + Width * Height);
            }
        }
    }
    /// <summary>
    /// Calculates the FPSA nd processing time.
    /// </summary>
    protected void CalculateFPSAndProcessingTime()
    {
        fpsCooldown += Time.deltaTime;
        frameCount++;
        if (fpsCooldown >= 1)
        {
            _fps = frameCount;
            frameCount = 0;
            fpsCooldown -= 1;
            CalculateProcessingTime();
        }
    }

    /// <summary>
    /// Calculates the processing time.
    /// </summary>
    protected void CalculateProcessingTime()
    {
        if (processing_time_list.Count > 0)
        {
            int sum = 0;
            for (int i = 0; i < processing_time_list.Count; i++)
            {
                sum += processing_time_list[i];
            }
            sum /= processing_time_list.Count;
            _processing_time = sum;
            processing_time_list.Clear();
        }
    }
    #endregion

    #region update_wrappers

    /// <summary>
    /// Processes the frame.
    /// </summary>
    protected void ProcessFrame()
    {
#if !UNITY_EDITOR
    	processFrame(ref hand_infos[0].hand_info,ref hand_infos[1].hand_info,ref manomotion_Session);
#endif

    }

    /// <summary>
    /// Calibrates the detection of the hand.
    /// Remember to follow the correct way of calibrating by having an open hand in a distance where the camera can see it.
    /// </summary>
    public void Calibrate()
    {
        SetFrameArray(_pixels);
#if !UNITY_EDITOR
	calibrate();
#endif
    }
    #endregion



}
