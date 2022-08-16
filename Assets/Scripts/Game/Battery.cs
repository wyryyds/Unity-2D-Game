using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float edge;
    private Transform _self;
    private float offsetY;

    void Start()
    {
        // 缓存Transform 优化
        _self = this.transform;

        // 边长
        edge = 1f;
    }

    public bool CanMoveToDir(Vector2 dir)
    {
        // 用来调试看射线的
        // DebugDarwLine(dir);

        // 不同情况的纵向偏差的处理
        if (dir == Vector2.up)
            offsetY = 0.01f;
        else if (dir == Vector2.down)
            offsetY = 0;

        RaycastHit2D hit;
        if (dir == Vector2.up || dir == Vector2.down)
        {
            hit = Physics2D.Raycast(_self.position + new Vector3(0.9f * edge, offsetY, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + new Vector3(-0.9f * edge, offsetY, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
        }
        else
        {
            hit = Physics2D.Raycast(_self.position + new Vector3(0, 0.9f * edge, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + (Vector3)dir * edge, dir, 0.001f * edge);
            if (!hit) hit = Physics2D.Raycast(_self.position + new Vector3(0, -0.9f * edge, 0) + (Vector3)dir * edge, dir, 0.001f * edge);
        }

        if (!hit || hit.collider.CompareTag("Socket") || hit.collider.tag == "Door")
        {
            GameObject.FindObjectOfType<Rewind>().StackAdd();
            _self.Translate(5 * Time.deltaTime * dir);
            return true;
        }

        return false;
    }

    void DebugDarwLine(Vector2 dir)
    {
        if (dir == Vector2.up || dir == Vector2.down)
        {

            Debug.DrawLine(_self.position + new Vector3(0.9f * edge, offsetY, 0) + (Vector3)dir * edge, _self.position + new Vector3(0.9f * edge, offsetY, 0) + (Vector3)dir * edge * 1.001f, new Color(255, 0, 0, 255));
            Debug.DrawLine(_self.position + new Vector3(-0.9f * edge, offsetY, 0) + (Vector3)dir * edge, _self.position + new Vector3(-0.9f * edge, offsetY, 0) + (Vector3)dir * edge * 1.001f, new Color(255, 0, 0, 255));
        }
        else
        {
            Debug.DrawLine(_self.position + new Vector3(0, 0.9f * edge, 0) + (Vector3)dir * edge, _self.position + new Vector3(0, 0.9f * edge, 0) + (Vector3)dir * 1.001f, new Color(255, 0, 0, 255));
            Debug.DrawLine(_self.position + new Vector3(0, -0.9f * edge, 0) + (Vector3)dir * edge, _self.position + new Vector3(0, -0.9f * edge, 0) + (Vector3)dir * 1.001f, new Color(255, 0, 0, 255));
        }
    }
}
