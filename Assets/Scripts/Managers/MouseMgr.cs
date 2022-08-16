using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseMgr : MonoBehaviour
{
    private static MouseMgr instance;
    public static MouseMgr Instance => instance;

    public Texture2D normal, click;

    private Camera _main;

    void Awake()
    {
        instance = this;
        _main = Camera.main;
        // 设置光标
        Cursor.SetCursor(normal, new Vector2(1, 1), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void InitEventTrigger(Button obj)
    {
        // 动态给按钮添加 EventTrigger
        EventTrigger et = obj.gameObject.AddComponent<EventTrigger>();
        // 创建对应的鼠标移上去的事件
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) =>
        {
            Cursor.SetCursor(click, new Vector2(1, 1), CursorMode.Auto);
        });
        et.triggers.Add(entry);

        // 创建对应的鼠标移出的事件
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) =>
        {
            Cursor.SetCursor(normal, new Vector2(1, 1), CursorMode.Auto);
        });
        et.triggers.Add(entry);

        // 创建对应的鼠标抬起的事件
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) =>
        {
            Cursor.SetCursor(normal, new Vector2(1, 1), CursorMode.Auto);
        });
        et.triggers.Add(entry);
    }
}
