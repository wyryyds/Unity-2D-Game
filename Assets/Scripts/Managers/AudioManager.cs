using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance => instance;

    private AudioManager() { }

    public AudioClip[] audio_clips;

    // 按键音效
    public AudioSource btnAS;

    // 背景音乐
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        instance = this;
    }

    void Start()
    {
        // 获取到音效
        audioSource = this.GetComponent<AudioSource>();
    }

    public enum SoundEffect
    {
        none = -1,

        buttom = 0,
        UIresponse = 1,
        transDoor = 2,
        victoryClip = 3,
    }

    public void PlaySE(SoundEffect se)
    {
        GetComponent<AudioSource>().PlayOneShot(audio_clips[(int)se]);
    }

    public void PlayButton()
    {
        btnAS.Play();
    }
}
