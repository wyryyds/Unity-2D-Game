using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BasePanel<PausePanel>
{
    public Button btnResume;
    public Button btnSetting;
    public Button btnMenu;
    public Button btnOp;

    void Start()
    {
        btnResume.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButton();
            Resume();
        });

        btnSetting.onClick.AddListener(() =>
        {
            OpenSetting();
            AudioManager.Instance.PlayButton();
        });

        btnMenu.onClick.AddListener(() =>
        {
            OpenMenu();
            AudioManager.Instance.PlayButton();
        });

        btnOp.onClick.AddListener(() =>
        {
            OpenOp();
            AudioManager.Instance.PlayButton();
        });

        MouseMgr.Instance.InitEventTrigger(btnResume);
        MouseMgr.Instance.InitEventTrigger(btnSetting);
        MouseMgr.Instance.InitEventTrigger(btnMenu);
        MouseMgr.Instance.InitEventTrigger(btnOp);

        // 一开始隐藏自己
        HideMe();
    }

    public void Resume()
    {
        // 隐藏暂停面板
        HideMe();
        // 将时间流速恢复正常
        Time.timeScale = 1f;
    }

    public void OpenSetting()
    {
        // 打开游戏面板
        GamePanel.Instance.ShowMe();
        // 隐藏暂停面板
        HideMe();
    }

    public void OpenMenu()
    {
        // 回到主菜单界面
        SceneManager.LoadScene(0);
        // 将时间流速恢复正常
        Time.timeScale = 1f;
    }

    public void OpenOp()
    {
        // 打开操作面板
        OpPanel.Instance.ShowMe();
        // 隐藏暂停面板
        HideMe();
    }
}
