using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpPanel : BasePanel<OpPanel>
{
    public Image op;
    public Button btnClose;

    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            Close();
            AudioManager.Instance.PlayButton();
        });
        btnClose.onClick.AddListener(AudioManager.Instance.PlayButton);

        MouseMgr.Instance.InitEventTrigger(btnClose);

        // 一开始 隐藏op面板
        HideMe();
    }

    void Close()
    {
        // 隐藏Op面板
        HideMe();

        // 打开暂停面板
        PausePanel.Instance.ShowMe();
    }
}
