using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnPalmCentre : MonoBehaviour
{

    public GameObject objectPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spwanOnPalmCentre(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
        // spwanOnIndexFingure(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);


    }

    void spwanOnPalmCentre(TrackingInfo trackingInfo)
    {

        Vector3 normalizedPalmCentre = trackingInfo.palm_center;
        float depth = trackingInfo.relative_depth;

        Vector3 relativePalmCentrePosition = ManoUtils.Instance.CalculateNewPosition(normalizedPalmCentre, depth);
        float smoothingVariable = 0.85f;
        // Rigidbody objectSpwaned = (Rigidbody)Instantiate.objectPrefab(objectPrefab, relativePalmCentrePosition, transform.rotation);



        objectPrefab.transform.position = Vector3.Lerp(transform.position, relativePalmCentrePosition, smoothingVariable);

    }






}
