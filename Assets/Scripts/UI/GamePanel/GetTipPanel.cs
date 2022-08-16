using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTipPanel : BasePanel<GetTipPanel>
{
    public Button btnSure;
    public Text tip;
    public Image loading;

    void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            GetPanel.Instance.ShowMe();
            AudioManager.Instance.PlayButton();
            HideMe();
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

    public override void HideMe()
    {
        // 并将界面重置为一开始加载的效果
        tip.text = "正在加载";
        loading.gameObject.SetActive(true);
        btnSure.gameObject.SetActive(false);
        base.HideMe();
    }
}
