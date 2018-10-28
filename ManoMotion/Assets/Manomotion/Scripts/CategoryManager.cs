using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    private static CategoryManager instance;
    public static CategoryManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }


    [SerializeField]
    GameObject[] categories;


    Vector2 categoryPosition;
    Category previousCategory;

    private void Awake()

    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PositionCategories();
 		ManoUtils.OnOrientationChanged += PositionCategories;
		UIRotationBehaviour.OnMenuEnabled += PositionCategories;
    }

    /// <summary>
    /// Positions the categories.
    /// </summary>
    void PositionCategories()
    {
		
        StartCoroutine(PositionCategoriesAfter(0.2f));
    }

    /// <summary>
    /// Positions the categories after a delay.
    /// </summary>
    /// <returns>The categories after.</returns>
    /// <param name="time">Requires a float value of delay.</param>
    IEnumerator PositionCategoriesAfter(float time)
    {

        yield return new WaitForSeconds(time);
        for (int i = 1; i < categories.Length; i++)
        {

            if (categories[i - 1].GetComponent<Category>())
            {
                previousCategory = categories[i - 1].GetComponent<Category>();
                categoryPosition = categories[i].GetComponent<RectTransform>().anchoredPosition;
                categories[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-previousCategory.categoryHeight + categories[i - 1].GetComponent<RectTransform>().anchoredPosition.y));

            }

        }

    }
}
