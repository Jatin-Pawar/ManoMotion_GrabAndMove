using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManoMotionManagerARCore : ManomotionManager
{

    private int ARCoreFrameWidth;
    private int ARCoreFrameHeight;
    Color32[] framePixels;
    int MAXIMUM_FRAME_SIZE = 266400;
    [SerializeField]
    RenderTexture render_texture;

    Texture2D frameTexture;
    // Use this for initialization

    private void ScaleRenderTexture()
    {
        int starting_width = Screen.width;
        int starting_height = Screen.height;

        ARCoreFrameWidth = starting_width;
        ARCoreFrameHeight = starting_height;

        int minimum_divisor = 2;
        while (ARCoreFrameHeight * ARCoreFrameWidth > MAXIMUM_FRAME_SIZE)
        {
            ARCoreFrameWidth = starting_width / minimum_divisor;
            ARCoreFrameHeight = starting_height / minimum_divisor;
            minimum_divisor++;
        }
        render_texture.Release();

        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            render_texture.width = ARCoreFrameWidth;
            render_texture.height = ARCoreFrameHeight;

        }
        else
        {
            render_texture.width = ARCoreFrameHeight;
            render_texture.height = ARCoreFrameWidth;
        }



    }

    private void InitializeManoMotionManagerARcore()
    {
        ScaleRenderTexture();
        frameTexture = new Texture2D(render_texture.width, render_texture.height);
        framePixels = new Color32[render_texture.width * render_texture.height];
    }
    new void Start()
    {
        InitializeManoMotionManagerARcore();
        manomotion_Session.add_on = AddOn.AR_CORE;
        PickResolution(render_texture.width, render_texture.height);
        SetUnityConditions();


    }

    new void Update()
    {
        ProcessARCoreFrame();

    }

    //Pick resolution when the orientation is changed. The dimensions are calculated based on the orientation
    void PickResolutionEvent()
    {
        ScaleRenderTexture();
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            PickResolution(ARCoreFrameWidth, ARCoreFrameHeight);

        }
        else
        {
            PickResolution(ARCoreFrameHeight, ARCoreFrameWidth);

        }

    }


    void ProcessARCoreFrame()
    {
        if (!render_texture)
        {
            Debug.Log("Missing render clone - Render Texture input");
            return;
        }
        UpdateFrame();
        UpdateOrientation();
        CalculateFPSAndProcessingTime();
        UpdatePixelValues();
        ProcessManomotion();
        UpdateTexturesWithNewInfo();
    }

    void UpdateFrame()
    {

        RenderTexture.active = render_texture;
        frameTexture.ReadPixels(new Rect(0, 0, RenderTexture.active.width, RenderTexture.active.height), 0, 0);
        frameTexture.Apply();
    }

    protected override void UpdatePixelValues()
    {
        framePixels = frameTexture.GetPixels32();
        if (Width != frameTexture.width || Height != frameTexture.height)
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                PickResolution(frameTexture.height, frameTexture.width);
            }
            else
            {
                PickResolution(frameTexture.width, frameTexture.height);

            }
            frameTexture.Resize(Width, Height);
            UpdateFrame();
        }
        if (framePixels.Length != Width * Height)
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                PickResolution(frameTexture.height, frameTexture.width);
            }
            else
            {
                PickResolution(frameTexture.width, frameTexture.height);

            }
        }

        framePixels.CopyTo(_pixels, 0);
    }




}
