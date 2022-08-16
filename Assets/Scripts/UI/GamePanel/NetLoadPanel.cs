using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 关卡类型
public enum E_Level_Type
{
    // 正常的 有留言板界面的
    Normal,
    // 特殊的 只有网络断开界面
    Special,
    // 最后一关
    Last,
}

public class NetLoadPanel : BasePanel<NetLoadPanel>
{
    public Button btnSure;
    public E_Level_Type level_Type;
    public Text tip;
    public Image loading;
    // 当前关卡是否有要显示点赞的trick
    public bool IsTrick;

    void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButton();
            Next();
        });

        MouseMgr.Instance.InitEventTrigger(btnSure);

        // 一到场景就打开留言加载面板
        ShowMe();
    }

    public void LoadOver()
    {
        tip.text = "加载完毕";
        loading.gameObject.SetActive(false);
        btnSure.gameObject.SetActive(true);
    }

    public void Next()
    {
        // 根据本关卡的不同类型来打开不同的面板
        switch (level_Type)
        {
            case E_Level_Type.Last:
            case E_Level_Type.Normal:
                // 打开留言面板
                MsgPanel.Instance.ShowMe();
                // 当前关卡有trick 并且 玩家已经留言 则同时打开 点赞面板
                if (IsTrick && GameDataMgr.Instance.IsMsg)
                    TrickPanel.Instance.ShowMe();
                break;
            case E_Level_Type.Special:
                // 若仍为重连 则打开网络断开面板
                NetCutPanel.Instance.ShowMe();
                break;
        }
        // 然后将本面板关闭
        HideMe();
    }

    public override void ShowMe()
    {
        // 将玩家脚本关闭 防止玩家能进行操作
        PlayController.Instance.enabled = false;
        // 3s后显示加载完毕
        Invoke("LoadOver", 3f);
        base.ShowMe();
    }

    public override void HideMe()
    {
        // 将玩家脚本打开
        PlayController.Instance.enabled = true;
        // 并将界面重置为一开始加载的效果
        tip.text = "正在加载网上留言";
        loading.gameObject.SetActive(true);
        btnSure.gameObject.SetActive(false);
        base.HideMe();
    }
}
