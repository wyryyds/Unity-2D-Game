using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetTipPanel : BasePanel<NetTipPanel>
{
    public Button btnSure;
    public Text tip;
    public Image loading;

    void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButton();
            SceneManager.LoadScene(1);
        });

        MouseMgr.Instance.InitEventTrigger(btnSure);

        HideMe();
    }

    public void DelayButtonShow()
    {
        tip.text = "加载完毕";
        loading.gameObject.SetActive(false);
        btnSure.gameObject.SetActive(true);
    }

    public override void ShowMe()
    {
        Invoke("DelayButtonShow", 3f);
        base.ShowMe();
    }
}
