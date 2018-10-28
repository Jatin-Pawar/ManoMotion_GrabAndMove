using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Ball")
        {

            gameObject.GetComponent<Renderer>().material.color = Color.cyan;

        }

        if (col.gameObject.name == "Red_Ball")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }


    }


}
