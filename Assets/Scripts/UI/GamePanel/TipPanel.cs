using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TipPanel : BasePanel<TipPanel>
{
    public Button btnClose;
    public Button btnSure;

    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            Next();
            AudioManager.Instance.PlayButton();
        });

        btnSure.onClick.AddListener(() =>
        {
            // 标记为玩家已留言
            GameDataMgr.Instance.IsMsg = true;
            Next();
            AudioManager.Instance.PlayButton();
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
        PlayController.Instance.enabled = true;
    }
}
