using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 类型
public enum E_Type
{
    // 正常的
    Normal,
    // 被锁住的
    Locked,
}

public class Exit : MonoBehaviour
{
    // 当前出口类型
    public E_Type now_exit_Type;
    // 打开锁住的出口所需要的所有插座
    public List<GameObject> sockets;
    // 是否已经开启
    public bool isOpen;

    private int socketsNum;

    void Start()
    {
        if (now_exit_Type == E_Type.Locked)
            // 初始化所需插座总数
            socketsNum = sockets.Count;
        else isOpen = true;
    }

    void Update()
    {
        // 如果出口还未开启
        if (!isOpen)
        {
            if (CanOpen())
            {
                // 打开传送门
                isOpen = true;
                // 打开自己的动画
                this.GetComponent<Animator>().enabled = true;
            }
        }
    }

    // 是否可以打开出口
    bool CanOpen()
    {
        int nowNum = 0;
        for (int i = 0; i < socketsNum; ++i)
        {
            if (sockets[i].GetComponent<Socket>().isCharge)
                ++nowNum;
        }

        // 若打开所需要的所有插座都是充电状态 则可以打开
        if (nowNum == socketsNum)
            return true;
        // 否则不能
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果已解锁
        if (isOpen)
        {
            if (other.tag == "Player")
            {
                switch (NetLoadPanel.Instance.level_Type)
                {
                    case E_Level_Type.Normal:
                        TipPanel.Instance.ShowMe();
                        break;
                    case E_Level_Type.Special:
                        // 播放通过音效
                        AudioManager.Instance.PlaySE(AudioManager.SoundEffect.victoryClip);
                        // 加载下一关
                        LoadNext.Instance.NextLevel(SceneManager.GetActiveScene().buildIndex);
                        break;
                    case E_Level_Type.Last:
                        GetPanel.Instance.ShowMe();
                        break;
                }
            }
        }
    }
}
