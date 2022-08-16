using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MsgPanel : BasePanel<MsgPanel>
{
    // 要显示的留言
    public List<string> strings = new List<string>();
    // 留言板上用来显示留言的Text
    public Text[] msgs;

    // 来存放要获取的留言文本的文本文件
    TextAsset text;
    // 临时存放文本信息的字符串
    private string str;
    // 对应的留言文本路径
    private string path;
    // 当前获取到的文字在文本中的下标
    private int getPos;
    // 当前行留言的开始对应的文本下标
    private int beginID = 0;
    // 当前获取到的留言文本的总个数
    private int textLen;

    // 留言之间的间隔时间
    public float cd = 3f;
    // 留言中每个字之间的间隔时间
    public float interval = 0.17f;

    // 当前留言的数量
    private int num;
    // 当前留言对应的索引号
    private int nowNum;
    // 所对应的要显示的索引号
    private int nowShow;

    // 留言间隔的计时器
    private float timerCD;
    // 留言中的字间隔时间的计时器
    private float timerInterval;
    // 在当前留言的下标
    private int pos;
    // 当前留言的长度
    private int len;
    // 用来存储当前留言的临时字符串
    private string strMsg;
    // 当前需要进行操作的文本下标
    private int nowIndex;
    // 是否可以进行初始化
    private bool canInit;

    void Start()
    {
        path = "Word" + SceneManager.GetActiveScene().buildIndex;
        InitText();
        GetText();
        // 得到留言数
        num = strings.Count;
        canInit = true;
        HideMe();
    }

    void Update()
    {
        if (NetLoadPanel.Instance.level_Type == E_Level_Type.Normal)
        {
            timerCD += Time.deltaTime;
            // 若当前留言都显示完毕 则不再进行判断
            if (nowNum >= num)
                return;
            // 过了留言间的CD 则继续显示留言
            if (timerCD > cd)
            {
                if (canInit)
                    // 则初始化即将的打字效果
                    InitSend(nowIndex, nowNum);

                TypeSend(nowIndex);
            }
        }
    }

    // 获取留言文本的初始化操作
    void InitText()
    {
        text = Resources.Load<TextAsset>(path);
        str = text.text;
        textLen = str.Length;
    }

    // 获取留言文本 并且一行行分割添加到对应的string中以待显示
    void GetText()
    {
        while (pos < textLen)
        {
            while (pos < textLen && str[pos] != '\n')
                ++pos;
            if (pos <= textLen)
            {
                strings.Add(str.Substring(beginID, pos - beginID));
                beginID = ++pos;
            }
        }
    }

    /// <summary>
    /// 用来进行 实现打字效果显示留言 的初始化
    /// </summary>
    /// <param name="textID"> 当前要进行显示的文本下标 </param>
    /// <param name="msgID"> 当前要进行显示的留言下标 </param>
    void InitSend(int textID, int msgID)
    {
        // 已初始化 则不可以进行下一次的初始化
        canInit = false;
        pos = 0;
        len = strings[msgID].Length;
        msgs[textID].text = "";
        strMsg = strings[msgID];

        // 如果当前留言的索引已经超过了了留言板的最大留言索引
        if (nowNum > 3)
        {
            // 后三条留言 则依次上移一条
            for (int i = 0; i < 3; ++i)
            {
                nowShow = nowNum - (3 - i);
                msgs[i].text = strings[nowShow];
                ChangeColor(i, nowShow);
            }
        }
    }

    /// <summary>
    /// 实现打字机效果 显示留言
    /// </summary>
    /// <param name="textID"> 要显示留言的文本下标 </param>
    void TypeSend(int textID)
    {
        ChangeColor(nowIndex, nowNum);
        timerInterval += Time.deltaTime;

        if (timerInterval > interval)
        {
            timerInterval = 0;
            ++pos;
            msgs[textID].text = strMsg.Substring(0, pos);
        }

        // 本条留言全部显示完
        if (pos >= len)
        {
            // 则重新等待留言间的CD
            timerCD = 0;
            // 当前留言下标也往后移一位
            ++nowNum;
            // 更新当前需要操作的文本索引
            nowIndex = nowNum > 3 ? 3 : nowNum;

            // 所有留言还未显示完毕
            if (nowNum < num)
            {
                // 则可以进行下一次打字效果的初始化
                canInit = true;
            }
        }
    }

    /// <summary>
    /// 根据对应留言的索引来得到独有信息来改变文本的颜色
    /// </summary>
    /// <param name="textID"> 文本索引 </param>
    /// <param name="msgID"> 留言索引 </param>
    private void ChangeColor(int textID, int msgID)
    {
        if (strings[msgID][0] == '喵')
            msgs[textID].color = new Color(1f, 0f, 0f, 1f);
        else if (strings[msgID][0] == '1')
            msgs[textID].color = new Color(0f, 1f, 0f, 1f);
        else
            msgs[textID].color = new Color(0f, 0f, 1f, 1f);
    }
}
