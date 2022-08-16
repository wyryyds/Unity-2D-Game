using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalRandom : MonoBehaviour
{
    private Image signal;

    public List<Sprite> images = new List<Sprite>();
    private int num;
    private float timer;
    public float cd = 25f;

    void Start()
    {
        signal = this.GetComponent<Image>();
        num = images.Count;
        // 初始化随机一张信号图
        this.GetComponent<Image>().sprite = images[Random.Range(0, num)];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > cd)
        {
            timer = 0;
            // 随机一张信号图
            this.GetComponent<Image>().sprite = images[Random.Range(0, num)];
        }
    }
}
