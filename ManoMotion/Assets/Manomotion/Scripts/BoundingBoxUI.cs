using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoundingBoxUI : MonoBehaviour
{
    [SerializeField]
    TextMesh top_left, width, height;
    [SerializeField]
    LineRenderer bound_line_renderer;
    private ManoUtils mano_utils;


    private void Start()
    {
        mano_utils = ManoUtils.Instance;
        bound_line_renderer.positionCount = 4;
    }
    // Update is called once per frame
    public void UpdateInfo(BoundingBox bounding_box)
    {
        float depth = 1;

        bound_line_renderer.SetPosition(0, mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x, 1 - bounding_box.top_left.y, 1), depth));
        bound_line_renderer.SetPosition(1, mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x + bounding_box.width, 1 - bounding_box.top_left.y, 1), depth));
        bound_line_renderer.SetPosition(2, mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x + bounding_box.width, (1 - bounding_box.top_left.y - bounding_box.height), 1), depth));
        bound_line_renderer.SetPosition(3, mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x, (1 - bounding_box.top_left.y - bounding_box.height), 1), depth));

        top_left.transform.localPosition = mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x, 1.05f - bounding_box.top_left.y, 1), 1);

        top_left.text = "Top Left: " + "X: " + bounding_box.top_left.x.ToString("F2") + " Y: " + bounding_box.top_left.y.ToString("F2");
        top_left.transform.localRotation = Camera.main.transform.localRotation;

        height.transform.localPosition = mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x + bounding_box.width + 0.025f, (1 - bounding_box.top_left.y - bounding_box.height / 2f), 1), 1);

        height.GetComponent<TextMesh>().text = "Height: " + bounding_box.height.ToString("F2");
        height.transform.localRotation = Camera.main.transform.localRotation;

        width.transform.localPosition = mano_utils.CalculateNewPosition(new Vector3(bounding_box.top_left.x + bounding_box.width / 2f, (1 - bounding_box.top_left.y - bounding_box.height) - 0.025f, 1), 1);

        width.GetComponent<TextMesh>().text = "Width: " + bounding_box.width.ToString("F2");
        width.transform.localRotation = Camera.main.transform.localRotation;

    }
}
