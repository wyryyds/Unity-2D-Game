using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic instance;
    public static BkMusic Instance => instance;

    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
        // 得到挂载的背景音乐
        audioSource = GetComponent<AudioSource>();
        // 初始化调节音量大小
        audioSource.volume = GameDataMgr.Instance.MusicData.bkVol;
    }

    // 提供给外部一个修改背景音乐大小的方法
    public void SetBkVolume(float value)
    {
        audioSource.volume = value;
    }
}
