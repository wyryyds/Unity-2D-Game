using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel<GamePanel>
{
    public Button btnClose;
    public Slider sldVol;
    public Slider sldSound;

    void Start()
    {
        // 先设置对应的数据
        sldVol.value = GameDataMgr.Instance.MusicData.bkVol;
        sldSound.value = GameDataMgr.Instance.MusicData.sound;

        btnClose.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButton();
            Close();
        });

        MouseMgr.Instance.InitEventTrigger(btnClose);

        sldVol.onValueChanged.AddListener(VolChanged);
        sldSound.onValueChanged.AddListener(SoundChanged);

        // 一开始隐藏设置面板
        HideMe();
    }

    void Close()
    {
        // 隐藏游戏面板
        HideMe();
        // 打开暂停面板
        PausePanel.Instance.ShowMe();
    }

    void VolChanged(float value)
    {
        GameDataMgr.Instance.SetBkVolume(value);
    }

    void SoundChanged(float value)
    {
        GameDataMgr.Instance.MusicData.sound = value;
    }

    public override void HideMe()
    {
        GameDataMgr.Instance.SaveMusicData();
        base.HideMe();
    }
}
