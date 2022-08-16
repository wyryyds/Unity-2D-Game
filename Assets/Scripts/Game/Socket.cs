using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public AudioSource audioSou;
    public AudioClip chargeMusic01, chargeMusic02;
    // 是否处于充电状态
    public bool isCharge;

    void Update()
    {
        // 将音量调节为当前音效音量大小
        audioSou.volume = GameDataMgr.Instance.MusicData.sound;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果电池碰到插座
        if (other.tag == "Battery")
        {
            // 将电池移动到插座上
            other.transform.position = transform.position;
            // 标记为充电状态
            isCharge = true;
            StartCoroutine(PlayMusic(chargeMusic01));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 如果电池离开插座
        if (other.tag == "Battery")
        {
            // 标记为未充电状态
            isCharge = false;
        }
    }

    IEnumerator PlayMusic(AudioClip cl) //协程控制两个音效先后播放。
    {
        audioSou.PlayOneShot(chargeMusic01);
        yield return new WaitForSeconds(cl.length);
        audioSou.PlayOneShot(chargeMusic02);
    }
}
