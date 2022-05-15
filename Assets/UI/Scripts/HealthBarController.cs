using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private static Text _healthText;

    private static int _health;

    void Awake()
    {
        _healthText = GameObject.Find("NumberOfHearts").GetComponent<Text>();
    }

    public static void SetHealth(int initialHealth)
    {
        _health = initialHealth >= 0 ? initialHealth : 0;
        _healthText.text = _health.ToString();
    }
}
