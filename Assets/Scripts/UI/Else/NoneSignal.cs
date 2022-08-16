using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoneSignal : MonoBehaviour
{
    private static NoneSignal instance;
    public static NoneSignal Instance => instance;

    private Image image;

    void Awake()
    {
        instance = this;
        image = this.GetComponent<Image>();
    }

    public void Open()
    {
        image.enabled = true;
    }
}
