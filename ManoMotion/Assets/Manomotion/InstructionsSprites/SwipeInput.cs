using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [SerializeField]
    float minimulSwipeDistance;
    Vector2 touchPosition;

    private void Update()
    {
        DetectSwipe();
    }


    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {

                touchPosition = Input.touches[0].position;
               

            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(Vector2.Distance(touchPosition, Input.touches[0].position)) >minimulSwipeDistance)
                {
                    EvaluateTouch(Input.touches[0].position);
                    touchPosition = Input.touches[0].position;
                  
                }
            }
        }
    }

    private void EvaluateTouch(Vector2 releasePosition)
    {
       
        if (touchPosition.x<releasePosition.x )
        {
            Debug.Log(Time.time + " Called Decrease Slide");
            InstructionsManager.Instance.DecreaseSlide();
        }
        else if (touchPosition.x >releasePosition.x)
        {
            InstructionsManager.Instance.IncreaseSlide();
 
        }



    }
}