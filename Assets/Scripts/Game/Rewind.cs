using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    public List<GameObject> CanRewind;
    public Stack<List<Vector3>> s = new Stack<List<Vector3>>();

    List<Vector3> list;

    public void StackAdd()
    {
        List<Vector3> tempList = new List<Vector3>();
        foreach (var item in CanRewind)
        {
            tempList.Add(item.transform.position);
        }
        s.Push(tempList);
    }

    public void Back()
    {
        if (s.Count > 0 && Time.timeScale == 1f)
        {
            list = s.Pop();
            for (int i = 0; i < list.Count; ++i)
                CanRewind[i].transform.position = list[i];
        }
    }


    void Update()
    {
        // 按R建回溯
        if (Input.GetKey(KeyCode.R))
            Back();
    }
}
