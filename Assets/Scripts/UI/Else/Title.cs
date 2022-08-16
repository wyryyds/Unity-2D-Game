using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Move_Type
{
    vertical,
    both,
}

public class Title : MonoBehaviour
{
    // 计时器
    private float timer;

    // 竖直波动倍速
    public float vSpeed;
    // 水平波动倍速
    public float hSpeed;
    // 竖直移动幅度
    public float vA;
    // 水平移动幅度
    public float hA;
    // 移动类型
    public E_Move_Type move_Type;

    void Update()
    {
        timer += Time.deltaTime;
        switch (move_Type)
        {
            case E_Move_Type.vertical:
                this.transform.Translate(Time.deltaTime * vA * Mathf.Sin(timer * vSpeed) * Vector3.up, 0);
                break;
            case E_Move_Type.both:
                this.transform.Translate(Time.deltaTime * hA * Mathf.Cos(timer * hSpeed), Time.deltaTime * vA * Mathf.Sin(timer * vSpeed), 0);
                break;
        }
    }
}
