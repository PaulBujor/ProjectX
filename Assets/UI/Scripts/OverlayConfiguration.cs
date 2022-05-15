using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayConfiguration : MonoBehaviour
{
    [Header("Game objects")]
    [Tooltip("The object whose visibility will be turned on/off")]
    [SerializeField]
    private GameObject _overlay;
    [SerializeField]
    private GameObject _bannerText;
    [SerializeField]
    private GameObject _timeValue;
    [Header("Configurations")]
    [Tooltip("The text that will be displayed on the banner")]
    [SerializeField]
    private string _menuTitle;

    void Awake()
    {
        _bannerText.GetComponent<Text>().text = _menuTitle;
    }


    public void SetOverlayVisible(bool visible)
    {
        if (visible)
        {
            _timeValue.GetComponent<Text>().text = GetTimeSinceStart();
        }

        _overlay.SetActive(visible);
    }

    private string GetTimeSinceStart()
    {
        var levelName = SceneManager.GetActiveScene().name.ToLower();
        var time = DateTime.Parse(PlayerPrefs.GetString(levelName + "StartTime"));
        var timeSinceStart = DateTime.Now - time;
        var timeSinceStartString = timeSinceStart.ToString("mm\\:ss");
        return timeSinceStartString;
    }
}
