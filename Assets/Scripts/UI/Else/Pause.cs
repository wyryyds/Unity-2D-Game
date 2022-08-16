using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private Button btnPause;

    void Start()
    {
        btnPause = this.GetComponent<Button>();
        btnPause.onClick.AddListener(OpenPause);
        btnPause.onClick.AddListener(AudioManager.Instance.PlayButton);
    }

    void OpenPause()
    {
        Time.timeScale = 0f;
        PausePanel.Instance.ShowMe();
    }
}
