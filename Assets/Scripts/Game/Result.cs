using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    private float edge;
    private Transform _self;

    void Start()
    {
        // 缓存Transform 优化
        _self = this.transform;

        // 边长
        edge = 0.954f;
    }

    public bool CanMoveToDir(Vector2 dir)
    {
        // 用来调试看射线的
        DebugDarwLine(dir);

        RaycastHit2D hit;
        if (dir == Vector2.up || dir == Vector2.down)
        {
            hit = Physics2D.Raycast(_self.position + new Vector3(0.87f * edge, 0, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + new Vector3(-0.87f * edge, 0, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
        }
        else
        {
            hit = Physics2D.Raycast(_self.position + new Vector3(0, 0.87f * edge, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + new Vector3(0, -0.87f * edge, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
        }

        if (!hit)
        {
            GameObject.FindObjectOfType<Rewind>().StackAdd();
            transform.Translate(5 * Time.deltaTime * dir);
            return true;
        }

        return false;
    }

    void DebugDarwLine(Vector2 dir)
    {
        if (dir == Vector2.up || dir == Vector2.down)
        {
            Debug.DrawLine(_self.position + new Vector3(0.87f * edge, 0, 0) + (Vector3)dir * edge, _self.position + new Vector3(0.87f * edge, 0, 0) + (Vector3)dir * edge * 2f, new Color(255, 0, 0, 255));
            Debug.DrawLine(_self.position + new Vector3(-0.87f * edge, 0, 0) + (Vector3)dir * edge, _self.position + new Vector3(-0.87f * edge, 0, 0) + (Vector3)dir * edge * 2f, new Color(255, 0, 0, 255));
        }
        else
        {
            Debug.DrawLine(_self.position + new Vector3(0, 0.87f * edge, 0) + (Vector3)dir * edge, _self.position + new Vector3(0, 0.87f * edge, 0) + (Vector3)dir * edge * 2f, new Color(255, 0, 0, 255));
            Debug.DrawLine(_self.position + new Vector3(0, -0.87f * edge, 0) + (Vector3)dir * edge, _self.position + new Vector3(0, -0.87f * edge, 0) + (Vector3)dir * edge * 2f, new Color(255, 0, 0, 255));
        }
    }
}
