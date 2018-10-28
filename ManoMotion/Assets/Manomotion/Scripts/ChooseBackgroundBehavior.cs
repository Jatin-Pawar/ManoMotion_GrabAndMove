using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChooseBackgroundBehavior : MonoBehaviour
{
    #region Singleton
    private static ChooseBackgroundBehavior _instance;
    public static ChooseBackgroundBehavior Instance
    {
        get
        {
            return _instance;
        }

    }
    #endregion

    [SerializeField]
    GameObject availableBackgrounds;

    [SerializeField]
    Button[] backgroundModeIcons;

    [SerializeField]
    Button backgroundSelectorIcon;


    public bool backgroundMenuIsOpen;

    bool coroutineIsRunning;


    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        backgroundMenuIsOpen = true;
        if (ManomotionManager.Instance.Manomotion_Session.add_on == AddOn.DEFAULT)
        {
            StartCoroutine("CloseAvailableBackgroundMenuAfter");
        }

    }

    /// <summary>
    /// Closes the available background menu.
    /// </summary>
    public void CloseAvailableBackgroundMenu()
    {
        Debug.Log("Executed Close Available Background Menu");
        availableBackgrounds.SetActive(false);
        backgroundMenuIsOpen = false;
        if (coroutineIsRunning)
        {
            StopCoroutine("CloseAvailableBackgroundMenuAfter");
        }
    }

    /// <summary>
    /// Toggles the available backgrounds menu.
    /// </summary>
    public void ToggleAvailableBackgroundsMenu()
    {
        backgroundMenuIsOpen = !backgroundMenuIsOpen;

        availableBackgrounds.SetActive(backgroundMenuIsOpen);
        if (backgroundMenuIsOpen)
        {
            StartCoroutine(CloseAvailableBackgroundMenuAfter());

        }

    }
    /// <summary>
    /// Closes the available background menu after 5 seconds.
    /// </summary>

    public IEnumerator CloseAvailableBackgroundMenuAfter()
    {
        coroutineIsRunning = true;
        yield return new WaitForSeconds(5f);
        CloseAvailableBackgroundMenu();
        coroutineIsRunning = false;

    }
}
