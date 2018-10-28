using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoUtils : MonoBehaviour
{
    #region Singleton
    private static ManoUtils instance;
    public delegate void OrientationChange();
    public static event OrientationChange OnOrientationChanged;

    public ScreenOrientation currentOrientation;

    public static ManoUtils Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    private Vector3 correction_ratio = Vector3.one;

    protected void Awake()
    {

        if (instance == null)
            instance = this;
        if (!cam)
            cam = Camera.main;
    }

    [SerializeField]
    private Camera cam;

    /// <summary>
    /// Calculates the new position in relation to the main camera.
    /// </summary>
    /// <param name="Point">Requires a Vector3 point</param>
    /// <param name="depth">Requires the float value of depth</param>
    /// <returns></returns>
    internal Vector3 CalculateNewPosition(Vector3 Point, float depth)
    {
        Vector3 correct_point = Point - Vector3.one * 0.5f;
        correct_point.Scale(correction_ratio);
        correct_point = correct_point + Vector3.one * 0.5f;
        correct_point = new Vector3(Mathf.Clamp(correct_point.x, 0, 1), Mathf.Clamp(correct_point.y, 0, 1), Mathf.Clamp(correct_point.z, 0, 1));
        return cam.ViewportToWorldPoint(correct_point + Vector3.forward * depth);
    }

    /// <summary>
    /// Adjust the transform in the received mesh renderer to fit the screen without stretching
    /// </summary>
    /// <param name="mesh_renderer"></param>
    internal void AjustBorders(MeshRenderer mesh_renderer, Session session)
    {

        float ratio = CalculateRatio(mesh_renderer, session);
        float size = CalculateSize(mesh_renderer, session, ratio);
        AdjustMeshScale(mesh_renderer, session, ratio, size);
        CalculateCorrectionPoint(mesh_renderer, session, ratio, size);


    }

    internal float CalculateRatio(MeshRenderer mesh_renderer, Session session)
    {
        float ratio = 1;

        switch (session.add_on)
        {
            case AddOn.DEFAULT:
                if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
                {

                    ratio = (float)ManomotionManager.Instance.Height / ManomotionManager.Instance.Width;
                }
                else
                {
                    ratio = (float)ManomotionManager.Instance.Width / ManomotionManager.Instance.Height;

                }
                break;
            case AddOn.AR_KIT:
                if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
                {

                    ratio = (float)ManomotionManager.Instance.Height / ManomotionManager.Instance.Width;
                }
                else
                {
                    ratio = (float)ManomotionManager.Instance.Width / ManomotionManager.Instance.Height;

                }

                break;
            case AddOn.AR_CORE:
                ratio = (float)ManomotionManager.Instance.Width / ManomotionManager.Instance.Height;

                break;
            case AddOn.VUFORIA:
                break;
            default:

                break;
        }
        return ratio;

    }
    internal float CalculateSize(MeshRenderer mesh_renderer, Session session, float ratio)
    {
        float size = 1;
        switch (session.add_on)
        {
            case AddOn.DEFAULT:
                {
                    float height = 2.0f * Mathf.Tan(0.5f * cam.fieldOfView * Mathf.Deg2Rad) * mesh_renderer.transform.localPosition.z;
                    if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
                    {
                        size = height;

                    }
                    else
                    {
                        //size = height * ratio;
                        float width = height * Screen.width / Screen.height;
                        size = width / ratio;
                    }


                    break;
                }
            case AddOn.AR_KIT:
                {
                    size = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * mesh_renderer.transform.localPosition.z;
                    float k = 16f * 1.25f / 9f;

                    size *= k / ratio;


                    break;
                }
            case AddOn.AR_CORE:
                {
                    size = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * mesh_renderer.transform.localPosition.z;
                    size *= 2;
                    if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
                    {
                        size /= ratio;
                    }
                    else
                    {
                    }
                    break;
                }
            case AddOn.VUFORIA:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        return size;
    }
    internal void AdjustMeshScale(MeshRenderer mesh_renderer, Session session, float ratio, float size)
    {
        if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
        {
            mesh_renderer.transform.localScale = new Vector3(size, size * ratio, 0f);
        }
        else
        {
            mesh_renderer.transform.localScale = new Vector3(size * ratio, size, 0f);

        }

    }
    internal void CalculateCorrectionPoint(MeshRenderer mesh_renderer, Session session, float ratio, float size)
    {
        switch (session.add_on)
        {
            case AddOn.DEFAULT:
                ///correct point
                if (session.orientation == SupportedOrientation.PORTRAIT || session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
                {
                    Vector3 screen_ratio = new Vector3(((float)Screen.height / Screen.width), 1, 1);
                    Vector3 image_ratio = new Vector3(ratio, 1, 1);
                    correction_ratio = Vector3.Scale(screen_ratio, image_ratio);
                }
                else
                {
                    Vector3 screen_ratio = new Vector3(1, 1 / ((float)Screen.height / Screen.width), 1);
                    Vector3 image_ratio = new Vector3(1, 1 / ratio, 1);
                    correction_ratio = Vector3.Scale(screen_ratio, image_ratio);
                }
                break;
            case AddOn.AR_KIT:

                break;
            case AddOn.AR_CORE:

                break;
            case AddOn.VUFORIA:
                break;
            default:

                break;
        }
    }

    private void Start()
    {
        currentOrientation = ScreenOrientation.Unknown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForScreenOrientationChange();

    }

    /// <summary>
    /// Checks for changes on the orientation of the device.
    /// </summary>
    void CheckForScreenOrientationChange()
    {
        if (currentOrientation != Screen.orientation)
        {
            currentOrientation = Screen.orientation;
            if (OnOrientationChanged != null)
            {
                OnOrientationChanged();

            }

        }

    }

    /// <summary>
    /// Retrieve the absolute values of a Vector3
    /// </summary>
    /// <returns>The abs.</returns>
    /// <param name="vector">Requires a Vector3 value.</param>
    Vector3 VectorAbs(Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    /// <summary>
    /// Properly orients a MeshRenderer in order to be displayed properly
    /// </summary>
    /// <param name="meshRenderer">Mesh renderer.</param>
    public void OrientMeshRenderer(MeshRenderer meshRenderer)
    {


        if (ManomotionManager.Instance.Manomotion_Session.add_on == AddOn.DEFAULT)
        {
            switch (ManomotionManager.Instance.Manomotion_Session.orientation)
            {

                case SupportedOrientation.PORTRAIT:
                    meshRenderer.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
                case SupportedOrientation.PORTRAIT_INVERTED:
                    meshRenderer.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case SupportedOrientation.LANDSCAPE_LEFT:
                    meshRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case SupportedOrientation.LANDSCAPE_RIGHT:
                    meshRenderer.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                default:
                    break;
            }
        }
        else
        {
            meshRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
