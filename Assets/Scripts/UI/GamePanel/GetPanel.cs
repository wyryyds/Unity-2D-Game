using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetPanel : BasePanel<GetPanel>
{
    public Text tip;
    public Button btnClose;
    public Button btnSure;

    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            Next();
            HideMe();
            AudioManager.Instance.PlayButton();
        });

        btnSure.onClick.AddListener(() =>
        {
            // 回到第七关加载界面
            GetTipPanel.Instance.ShowMe();
            AudioManager.Instance.PlayButton();
            // 关闭面板
            HideMe();
        });

        MouseMgr.Instance.InitEventTrigger(btnClose);
        MouseMgr.Instance.InitEventTrigger(btnSure);

        HideMe();
    }

    // 切换下一关
    void Next()
    {
        // 播放通过音效
        AudioManager.Instance.PlaySE(AudioManager.SoundEffect.victoryClip);
        // 加载下一关
        LoadNext.Instance.NextLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
