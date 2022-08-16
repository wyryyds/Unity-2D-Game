using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public List<Sprite> sprites;

    private int pos;
    private float value;
    // 当前Image的名字
    private string nowName;

    void Start()
    {
        // 获取当前Image的名字
        nowName = this.GetComponent<Image>().name;
    }

    void Update()
    {
        JudgeClass();
        JudgePos(value);
        this.GetComponent<Image>().sprite = sprites[pos];
    }

    // 判断当前所需的数据是哪个
    void JudgeClass()
    {
        if (nowName == "ImgVol")
            value = GameDataMgr.Instance.MusicData.bkVol;
        else if (nowName == "ImgSound")
            value = GameDataMgr.Instance.MusicData.sound;
    }

    // 判断当前状态对应的图片在List中的下标
    void JudgePos(float value)
    {
        if (value == 0f)
            pos = 0;
        else if (value < 0.33f)
            pos = 1;
        else if (value < 0.66f)
            pos = 2;
        else
            pos = 3;
    }
}
