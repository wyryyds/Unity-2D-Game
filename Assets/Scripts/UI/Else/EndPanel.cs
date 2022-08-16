using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : BasePanel<EndPanel>
{
    // 退出按钮
    public Button btnQuit;
    // 人员表
    public Text peopleList;
    // End
    public Text end;
    // Word
    public Text word;

    // 字的显示间隔时间
    public float timeInterval;
    // 按钮的框Image
    public Image imgBtnSide;
    // 按钮的Image
    public Image imgBtn;

    // 计时器
    private float timer;
    // 当前需要显示到的位置
    private int pos;
    // 获取需要显示的文本
    private string strPeople;
    // 获取需要显示的文本
    private string strEnd;
    // 获取需要显示的文本
    private string strWord;

    // 是否需要继续显示人员表
    private bool canShowPeople;
    // 是否需要继续显示End
    private bool canShowEnd;
    // 是否需要继续显示Word
    private bool canShowWord;

    // 是否打开按钮
    private bool openBtn;
    // 是否渐变
    private bool canSlow;
    // 是否第一次开始渐变
    private bool isFirst;
    // 淡出的插值
    private float valueOut;
    // 淡入的插值
    private float valueIn;

    void Start()
    {
        btnQuit.onClick.AddListener(QuitGame);
        btnQuit.onClick.AddListener(AudioManager.Instance.PlayButton);

        // 关闭按钮
        btnQuit.gameObject.SetActive(false);

        MouseMgr.Instance.InitEventTrigger(btnQuit);

        isFirst = true;

        canShowPeople = true;
        canShowEnd = false;
        strPeople = peopleList.text;
        peopleList.text = "";

        strEnd = end.text;
        end.text = "";

        strWord = word.text;
        word.text = "";
    }

    void Update()
    {
        // 开始显示人员表
        if (canShowPeople)
            ShowPoeple();
        // 开始显示Word
        if (canShowWord)
            ShowWord();
        // 开始显示End
        if (canShowEnd)
            ShowEnd();
        // 准备打开按钮
        if (openBtn)
        {
            if (canSlow)
                // 开始渐变
                SlowIn();
        }
    }

    void ShowPoeple()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            timer = 0;
            ++pos;
            peopleList.text = strPeople.Substring(0, pos);

            // 人员表显示完成
            if (pos >= strPeople.Length)
            {
                canShowPeople = false;
                // 初始化 准备显示Word
                canShowWord = true;
                timer = 0f;
                pos = 0;
            }
        }
    }

    void ShowWord()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            timer = 0;
            ++pos;
            word.text = strWord.Substring(0, pos);

            // End显示完成
            if (pos >= strWord.Length)
            {
                canShowWord = false;
                // 初始化 准备显示End
                canShowEnd = true;
                timer = 0f;
                pos = 0;
            }
        }
    }

    void ShowEnd()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            timer = 0;
            ++pos;
            end.text = strEnd.Substring(0, pos);

            // End显示完成
            if (pos >= strEnd.Length)
            {
                canShowEnd = false;
                openBtn = true;
                // 开始渐变
                canSlow = true;
            }
        }
    }

    // 淡入效果
    void SlowIn()
    {
        // 插值计算
        valueIn = Mathf.Lerp(valueIn, 1f, Time.deltaTime);

        // 近似计算
        if (1 - valueIn <= 0.2f)
            valueIn = 1f;
        // 透明度改变
        end.color = new Color(end.color.r, end.color.g, end.color.b, 1 - valueIn);

        // 插值完成
        if (valueIn == 1f)
        {
            // 关闭End
            end.gameObject.SetActive(false);
            // 开始淡出
            SlouOut();
            if (isFirst)
            {
                // 打开按钮 并设置为透明的
                btnQuit.gameObject.SetActive(true);
                imgBtnSide.color = new Color(imgBtnSide.color.r, imgBtnSide.color.g, imgBtnSide.color.b, 0);
                imgBtn.color = new Color(imgBtn.color.r, imgBtn.color.g, imgBtn.color.b, 0);
                isFirst = false;
            }
        }
    }

    // 淡出效果
    void SlouOut()
    {
        // 插值计算
        valueOut = Mathf.Lerp(valueOut, 1f, Time.deltaTime * 0.7f);
        // 近似计算
        if (1 - valueOut <= 0.1f)
            valueOut = 1f;

        // 透明度改变
        imgBtnSide.color = new Color(imgBtnSide.color.r, imgBtnSide.color.g, imgBtnSide.color.b, valueOut);
        imgBtn.color = new Color(imgBtn.color.r, imgBtn.color.g, imgBtn.color.b, valueOut);
        // 插值完成
        if (valueOut == 1f)
            // 将该传送门的淡出给禁止
            canSlow = false;
    }


    // 退出游戏
    public void QuitGame()
    {
        // 编辑模式下的退出
        // UnityEditor.EditorApplication.isPlaying = false;
        // 要打包时把编辑状态的退出换成下面的退出 ~
        Application.Quit();
    }
}
