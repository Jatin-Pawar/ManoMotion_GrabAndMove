using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingureTips : MonoBehaviour
{

    public GameObject objectPrefab;

    protected GameObject IndexFingureObject;

    // Use this for initialization
    void Start()
    {
        IndexFingureObject = Instantiate(objectPrefab);
        IndexFingureObject.GetComponent<Renderer>().material.color = Color.cyan;

    }

    // Update is called once per frame
    void Update()
    {

        spwanOnIndexFingure(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);

    }

    void spwanOnIndexFingure(TrackingInfo trackingInfo)
    {

        Vector3 indexFigurePosition = trackingInfo.finger_tips[3];
        float depth = trackingInfo.relative_depth;

        Vector3 relativeIndexFingurePosition = ManoUtils.Instance.CalculateNewPosition(indexFigurePosition, depth);
        float smoothingVariable = 0.85f;



        IndexFingureObject.transform.position = Vector3.Lerp(IndexFingureObject.transform.position, relativeIndexFingurePosition, smoothingVariable);



    }
}
