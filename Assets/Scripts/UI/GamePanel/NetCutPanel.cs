using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetCutPanel : BasePanel<NetCutPanel>
{
    public Text tip;
    public Button btnClose;
    public Button btnReLoad;

    // 是否可以重连上
    public bool canReLoad;
    // 当前需要重连的次数
    public int num = 1;

    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            if (!canReLoad)
                NoneSignal.Instance.Open();

            AudioManager.Instance.PlayButton();
            HideMe();
        });

        btnReLoad.onClick.AddListener(() =>
        {
            // 如果可以重连上 则每点一次重连 就减少需重连的次数
            if (canReLoad)
                --num;
            if (num == 0)
                // 如果当前重连次数 则将当前关卡模式改为正常的
                NetLoadPanel.Instance.level_Type = E_Level_Type.Normal;
            // 关闭面板
            HideMe();
            // 回到留言加载界面
            NetLoadPanel.Instance.ShowMe();
            AudioManager.Instance.PlayButton();
        });

        MouseMgr.Instance.InitEventTrigger(btnClose);
        MouseMgr.Instance.InitEventTrigger(btnReLoad);

        HideMe();
    }
}
