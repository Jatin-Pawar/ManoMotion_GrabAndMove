using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform
{
	UNITY_ANDROID,
	UNITY_IOS
};

public enum AddOn
{
	DEFAULT = 0,
	AR_KIT = 1,
	AR_CORE = 2,
	VUFORIA = 3
};

public enum SelectedHand
{
    RIGHT_HAND = 1,
    LEFT_HAND = 0,
    BOTH_HANDS = 2
};

public enum SupportedOrientation
{
    LANDSCAPE_LEFT = 3,
    LANDSCAPE_RIGHT = 4,
    PORTRAIT = 1,
    PORTRAIT_INVERTED = 2
};
public enum ImageFormat
{
    BGRA_IMAGE = 5,
    GRAYSCALE = 4,
    DEPTH_MAP = 3,
    YUV_IMAGE = 2,
    VUFORIA_IMAGE = 1,
    RGBA_IMAGE = 0
};
public enum Flags
{

    FLAG_CALIBRATING = 20,
    FLAG_CALIBRATION_SUCCESS = 21,
    FLAG_CALIBRATION_FAIL = 22,

    FLAG_LICENSE_OK = 30,
    FLAG_LICENSE_KEY_NOT_FOUND = 31,
    FLAG_LICENSE_EXPIRED_WARNING = 32,
    FLAG_LICENSE_INVALID_PLAN = 33,
    FLAG_LICENSE_KEY_BLOCKED = 34,
    FLAG_INVALID_ACCESS_TOKEN = 35,
    FLAG_LICENSE_ACCESS_DENIED = 36,
    FLAG_LICENSE_MAX_NUM_DEVICES = 37,
    FLAG_UNKNOWN_SERVER_REPLY = 38,
    FLAG_LICENSE_PRODUCT_NOT_FOUND = 39,
    FLAG_LICENSE_INCORRECT_INPUT_PARAMETER = 40,
    FLAG_LICENSE_INTERNET_REQUIRED = 41,
    FLAG_BOUNDLE_ID_DOESENT_MATCH = 42,
    TWO_HANDS_REQUESTED_BUT_NOT_ALLOWED = 50
};
public enum BackgroundMode
{
    BACKGROUND_NORMAL = 0,
    BACKGROUND_RED = 1,
    BACKGROUND_YELLOW = 2,
    BACKGROUND_BROWN_DARKRED = 3,
    BACKGROUND_AUTO = 4
};

public struct Session
{

    public Flags flag;
    public Platform current_plataform;
    public BackgroundMode background_mode;
    public ImageFormat image_format;
    public SupportedOrientation orientation;
    public int calibration_value;
	public SelectedHand hand_selection;
	public AddOn add_on;
	public int version;
    public int is_two_hands_enabled_by_developer;
    public int two_hands_supported;
}