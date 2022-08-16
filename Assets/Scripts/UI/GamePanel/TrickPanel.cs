using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrickPanel : BasePanel<TrickPanel>
{
    public Image bk;
    public Text trick;
    public Text tip;
    public string[] strings;
    public float showTime = 2.5f;
    public float showCD = 10f;

    private int num;
    private int nowIndex;
    private float timer;
    private Transform _bk;
    private bool canShow;
    private bool canHide;

    void Start()
    {
        //Test
        GameDataMgr.Instance.IsMsg = true;

        _bk = bk.transform;
        num = strings.Length;
        canShow = true;
        HideMe();
    }

    void Update()
    {
        // 如果显示完毕 则无需判断
        if (nowIndex >= num)
            return;

        if (timer < showCD || timer < showTime)
            timer += Time.deltaTime;

        if (canShow && timer > showCD)
        {
            // 显示对应点赞
            trick.text = strings[nowIndex];
            ShowTrick();
        }

        if (canHide && timer > showTime)
            HideTrick();
    }

    void ShowTrick()
    {
        _bk.localPosition = Vector3.Lerp(_bk.localPosition, new Vector3(_bk.localPosition.x, -475, _bk.localPosition.z), Time.deltaTime * 3);

        // 插值完成(弹出完成)
        if (Mathf.Abs(-475 - _bk.localPosition.y) < 1f)
        {
            canShow = false;
            timer = 0;
            // 下滑点赞
            canHide = true;
        }
    }

    void HideTrick()
    {
        _bk.localPosition = Vector3.Lerp(_bk.localPosition, new Vector3(_bk.localPosition.x, -615, _bk.localPosition.z), Time.deltaTime * 3);

        // 插值完成(下滑完成)
        if (Mathf.Abs(-615 - _bk.localPosition.y) < 1f)
        {
            canHide = false;
            timer = 0;
            nowIndex++;
            // 弹出点赞
            canShow = true;
        }
    }
}
