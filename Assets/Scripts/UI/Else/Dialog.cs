using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文件内容")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite facePlayer;
    public Sprite faceNPC;

    bool textFinished;

    List<string> textList = new List<string>();

    private void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.E)&&textFinished)
        {
            StartCoroutine(SetTextUI());
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textLabel.text = "";
        textFinished = false;

        switch (textList[index].Trim().ToString())
        {
            case "A":
                faceImage.sprite = facePlayer;
                index++;
                break;
            case "B":
                faceImage.sprite = faceNPC;
                index++;
                break;
        }

        for(int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
        index++;
    }
}
