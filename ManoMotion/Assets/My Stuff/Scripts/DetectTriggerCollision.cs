using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTriggerCollision : MonoBehaviour
{

    // Use this for initialization

    Color lastColor;


    void OnTriggerEnter(Collider trigCol)
    {
        lastColor = trigCol.gameObject.GetComponent<Renderer>().material.color;
        // trigCol.gameObject.GetComponent<Renderer>().material.color = Color.green;

    }

    void OnTriggerStay(Collider trigCol)
    {
        trigCol.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

    }

    void OnTriggerExit(Collider trigCol)
    {
        trigCol.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = lastColor;
        // trigCol.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

    }
}
