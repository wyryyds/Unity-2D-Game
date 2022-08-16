using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransDoor : MonoBehaviour
{
    // 当前传送门类型
    public E_Type now_Door_Type;

    // 另一个传送门的坐标
    public Transform destination;
    // 人物的坐标
    public Transform player;
    // 是否允许传送
    public bool canTrans;
    // 是否允许淡入
    public bool canSlow;

    // 打开所需要的所有插座
    public List<GameObject> sockets;
    // 是否已经开启
    public bool isOpen;

    // 人物的SpriteRenderer
    private SpriteRenderer image;
    private float valueOut;
    private float valueIn;
    private int socketsNum;

    void Start()
    {
        // 获取人物的SpriteRenderer
        image = player.GetComponent<SpriteRenderer>();

        if (now_Door_Type == E_Type.Locked)
            // 初始化所需插座总数
            socketsNum = sockets.Count;
        else isOpen = true;
    }

    void Update()
    {
        // 如果传送门还未开启过
        if (!isOpen)
        {
            // 判断是否可以打开传送门
            if (CanOpen())
            {
                // 打开传送门
                isOpen = true;
                // 允许自己和另一个传送门传送
                canTrans = true;
                destination.GetComponent<TransDoor>().canTrans = true;
                // 打开自己和另一个传送门的动画
                this.GetComponent<Animator>().enabled = true;
                destination.GetComponent<Animator>().enabled = true;
            }
        }

        // 如果可以淡出
        if (canSlow)
        {
            // 开始淡入
            SlowIn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 可以传送的话
        if (canTrans)
        {
            //  如果不是玩家在传送门
            if (other.tag != "Player")
                // 则直接传送
                other.transform.position = destination.position;

            else
            {
                // 允许淡出 并且将淡入淡出所需值置为0
                canSlow = true;
                valueOut = 0f;
                valueIn = 0f;
            }

            // 将另一个传送门的传送、淡出给禁止
            destination.GetComponent<TransDoor>().canTrans = false;
            destination.GetComponent<TransDoor>().canSlow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 如果传送门已开启
        if (isOpen)
        {
            // 如果该传送门不可以传送的话
            if (!canTrans)
            {
                // 将该传送门的传送给允许
                canTrans = true;
            }
        }
    }

    // 人物淡入效果
    void SlowIn()
    {
        // 插值计算
        valueIn = Mathf.Lerp(valueIn, 1f, Time.deltaTime * 2);

        // 近似计算
        if (1 - valueIn <= 0.3f)
            valueIn = 1f;
        // 透明度改变
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - valueIn);

        // 插值完成
        if (valueIn == 1f)
        {
            // 人物传送
            player.position = destination.position;
            // 开始淡出
            SlouOut();
        }
    }

    // 人物淡出效果
    void SlouOut()
    {
        // 插值计算
        valueOut = Mathf.Lerp(valueOut, 1f, Time.deltaTime * 2);
        // 近似计算
        if (1 - valueOut <= 0.3f)
            valueOut = 1f;
        // 透明度改变
        image.color = new Color(image.color.r, image.color.g, image.color.b, valueOut);
        // 插值完成
        if (valueOut == 1f)
            // 将该传送门的淡出给禁止
            canSlow = false;
    }

    // 是否可以打开传送门
    bool CanOpen()
    {
        int nowNum = 0;
        for (int i = 0; i < socketsNum; ++i)
        {
            if (sockets[i].GetComponent<Socket>().isCharge)
                ++nowNum;
        }

        // 若打开所需要的所有插座都是充电状态 则可以打开
        if (nowNum == socketsNum)
            return true;
        // 否则不能
        return false;
    }
}
