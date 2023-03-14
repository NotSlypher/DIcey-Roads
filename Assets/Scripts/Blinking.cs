using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{

    [SerializeField] Image sr;
    [SerializeField] Text tm;
    private float a = 1, z = 3;
    private bool increasing = true;
    Color color;
    Color colort;

    void Start()
    {
        sr = gameObject.GetComponent<Image>();
        tm = GameObject.FindObjectOfType<Text>();
        color = sr.color;
        colort = tm.color;
        a = 1;
    }



    void Update()
    {
        float t = Time.deltaTime;
        // if (a >= maximum) increasing = false;
        // if (a <= minimum) increasing = true;
        z -= t;
        if (z <= 0f)
        {
            a -= t;
            color.a = a;
            colort.a = a;
            sr.color = color;
            tm.color = colort;
        }
        
    }

}
