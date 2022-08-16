using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{
    public Button btnBegin;
    public Button btnSetting;
    public Button btnQuit;

    void Start()
    {
        btnBegin.onClick.AddListener(() =>
        {
            // 打开网络连接提示面板
            NetTipPanel.Instance.ShowMe();
            AudioManager.Instance.PlayButton();
        });

        btnSetting.onClick.AddListener(() =>
        {
            // 打开设置面板
            SettingPanel.Instance.ShowMe();
            AudioManager.Instance.PlayButton();
        });

        btnQuit.onClick.AddListener(QuitGame);

        // 为按钮动态添加改变光标的 EventTrigger
        MouseMgr.Instance.InitEventTrigger(btnBegin);
        MouseMgr.Instance.InitEventTrigger(btnSetting);
        MouseMgr.Instance.InitEventTrigger(btnQuit);
    }

    // 退出游戏
    public void QuitGame()
    {
        // 编辑模式下的退出
        // UnityEditor.EditorApplication.isPlaying = false;
        // 要打包时把编辑状态的退出换成下面的退出 ~
        Application.Quit();
        AudioManager.Instance.PlayButton();
    }
}
