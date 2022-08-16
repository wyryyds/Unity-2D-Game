using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理游戏数据
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    private MusicData musicData;
    public MusicData MusicData => musicData;

    // 玩家是否留言
    public bool IsMsg;

    private GameDataMgr()
    {
        // 游戏一开始获取上一次的音乐数据并初始化
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        // 初始化背景音乐大小
        BkMusic.Instance.SetBkVolume(musicData.bkVol);
    }

    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(MusicData, "MusicData");
    }

    public void SetBkVolume(float value)
    {
        musicData.bkVol = value;
        BkMusic.Instance.SetBkVolume(value);
    }
}
