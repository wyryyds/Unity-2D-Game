using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChange : MonoBehaviour
{
    // 音效
    private AudioSource audioSource;

    void Start()
    {
        // 获取到音效
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        // 将音量调节为当前音效音量大小
        audioSource.volume = GameDataMgr.Instance.MusicData.sound;
    }
}
