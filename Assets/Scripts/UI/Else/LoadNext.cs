using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNext : MonoBehaviour
{
    private static LoadNext instance;

    public static LoadNext Instance => instance;

    void Awake()
    {
        instance = this;
    }

    public Canvas loadPanel;
    public Image bk;
    public Image processBk;
    public Image loadAnim;
    public Transform loading;
    public Transform destination;
    public Text tip;
    // 加载时跳动的字体
    public GameObject load;
    // 整张地图
    public GameObject map;

    private AsyncOperation asyncOperation;
    private float loadProgress;
    private float walkProgress;
    private bool canLoad;
    private bool canSlow;
    private float value;

    void Update()
    {
        if (canLoad)
        {
            loadProgress = asyncOperation.progress;
            if (asyncOperation.progress != 0f)
            {
                // progress最大为0.9f
                if (asyncOperation.progress >= 0.9f)
                    loadProgress = 1f;

                // 进行插值更新
                if (walkProgress != asyncOperation.progress)
                {
                    walkProgress = Mathf.Lerp(walkProgress, asyncOperation.progress, Time.deltaTime);
                    loading.position = Vector3.Lerp(loading.position, destination.position, Time.deltaTime);
                    // 采用近似计算
                    if (asyncOperation.progress - walkProgress <= 0.1f)
                    {
                        walkProgress = loadProgress;
                        loading.position = destination.position;
                    }
                }

                // 当加载完成时打开自动切换
                if (walkProgress == 1f)
                {
                    Destroy(load.gameObject);
                    tip.text = "按空格键进入下一关";
                    if (Input.GetKeyDown(KeyCode.Space))
                        canSlow = true;
                }
            }
        }

        if (canSlow)
            Slow();
    }

    // 加载界面淡出效果
    void Slow()
    {
        // 插值运算
        value = Mathf.Lerp(value, 1f, Time.deltaTime);
        // 近似运算
        if (1f - value <= 0.1f)
            value = 1f;
        // 透明度改变
        bk.color = new Color(bk.color.r, bk.color.g, bk.color.b, 1 - value);
        tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, 1 - value);
        processBk.color = new Color(processBk.color.r, processBk.color.g, processBk.color.b, 1 - value);
        loadAnim.color = new Color(loadAnim.color.r, loadAnim.color.g, loadAnim.color.b, 1 - value);

        // 淡出完毕
        if (value == 1f)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }

    public void NextLevel(int id)
    {
        StartCoroutine(LoadScene(id));
    }

    IEnumerator LoadScene(int id)
    {
        canLoad = true;
        asyncOperation = SceneManager.LoadSceneAsync(id + 1);
        loadPanel.gameObject.SetActive(true);
        map.gameObject.SetActive(false);
        // 阻止加载完成自动切换
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
}
