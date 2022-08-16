using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    private static PlayController instance;

    public static PlayController Instance => instance;


    void Awake()
    {
        instance = this;
    }

    public Sprite sprite;
    public LayerMask detectLayer;
    public Transform player;
    public List<Transform> boxTranform;
    public AudioSource audioSource;

    private float height;
    private float width;
    private float offsetX;
    private float offsetY;
    private float disX = 5.2f;
    private float disY = 5.7f;
    private Animator ani;
    private Vector2 moveDir;
    private Transform _self;
    private Collider2D coll;
    

    void Start()
    {
        // 缓存Transform优化
        _self = transform;
        ani = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        // 人物的高
        height = 0.87f;
        // 人物的宽
        width = 0.59f;
        // 纵向的偏差
        offsetY = -0.1f;
    }

    void Update()
    {
        Move();

        SwitchAnimation();

        // 用来调试看射线的
        // DebugDarwLine();
    }

    void Move()
    {
        // 不同情况的横向偏差的处理
        if (_self.localScale.x < 0)
            offsetX = -0.1f;
        else
            offsetX = 0.1f;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveDir = Vector2.right;
            if (_self.localScale.x < 0)
            {
                _self.localScale = new Vector3(-1 * _self.localScale.x, _self.localScale.y, _self.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveDir = Vector2.left;
            if (_self.localScale.x > 0)
            {
                _self.localScale = new Vector3(-1 * _self.localScale.x, _self.localScale.y, _self.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveDir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveDir = Vector2.down;
        }

        if (moveDir != Vector2.zero)
        {
            if (CanMoveToDir(moveDir))
            {
                
                _self.Translate(5 * Time.deltaTime * moveDir);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                
               
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            moveDir = Vector2.zero;
        }
    }

    bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit1, hit2;
        if (dir == Vector2.up || dir == Vector2.down)
        {
            hit1 = Physics2D.Raycast(_self.position + new Vector3(0.85f * width + offsetX, offsetY, 0), dir, 1.1f * height, detectLayer);
            hit2 = Physics2D.Raycast(_self.position + new Vector3(0.85f * -width + offsetX, offsetY, 0), dir, 1.1f * height, detectLayer);
        }
        else
        {
            hit1 = Physics2D.Raycast(_self.position + new Vector3(0, 0.85f * height + offsetY, 0), dir, 1.1f * width, detectLayer);
            hit2 = Physics2D.Raycast(_self.position + new Vector3(0, 0.85f * -height + offsetY, 0), dir, 1.1f * width, detectLayer);
        }

        if (!hit1 && !hit2)
        {
            GameObject.FindObjectOfType<Rewind>().StackAdd();
            return true;
        }
        else if (!hit1)
        {
            if (hit2.collider.GetComponent<Box>() != null)
                return hit2.collider.GetComponent<Box>().CanMoveToDir(dir);
            else if (hit2.collider.GetComponent<Battery>() != null)
                return hit2.collider.GetComponent<Battery>().CanMoveToDir(dir);
            else if (hit2.collider.GetComponent<Because>() != null)
                return hit2.collider.GetComponent<Because>().CanMoveToDir(dir);
        }
        else if (!hit2)
        {
            if (hit1.collider.GetComponent<Box>() != null)
                return hit1.collider.GetComponent<Box>().CanMoveToDir(dir);
            else if (hit1.collider.GetComponent<Battery>() != null)
                return hit1.collider.GetComponent<Battery>().CanMoveToDir(dir);
            else if (hit1.collider.GetComponent<Because>() != null)
                return hit1.collider.GetComponent<Because>().CanMoveToDir(dir);
        }
        else
        {
            if (hit1.collider.GetComponent<Box>() != null && hit2.collider.GetComponent<Box>() != null)
                return hit1.collider.GetComponent<Box>().CanMoveToDir(dir);
            else if (hit1.collider.GetComponent<Battery>() != null && hit2.collider.GetComponent<Battery>() != null)
                return hit1.collider.GetComponent<Battery>().CanMoveToDir(dir);
            else if (hit1.collider.GetComponent<Because>() != null && hit2.collider.GetComponent<Because>() != null)
                return hit1.collider.GetComponent<Because>().CanMoveToDir(dir);
        }

        return false;
    }

    void DebugDarwLine()
    {
        if (moveDir == Vector2.up || moveDir == Vector2.down)
        {
            Debug.DrawLine(_self.position + new Vector3(0.85f * width + offsetX, offsetY, 0), _self.position + new Vector3(0.85f * width + offsetX, offsetY, 0) + 1.1f * height * (Vector3)moveDir);
            Debug.DrawLine(_self.position + new Vector3(0.85f * -width + offsetX, offsetY, 0), _self.position + new Vector3(0.85f * -width + offsetX, offsetY, 0) + 1.1f * height * (Vector3)moveDir);
        }
        else
        {
            Debug.DrawLine(_self.position + new Vector3(0, 0.85f * height + offsetY, 0), _self.position + new Vector3(0, 0.85f * height + offsetY, 0) + 1.1f * width * (Vector3)moveDir);
            Debug.DrawLine(_self.position + new Vector3(0, 0.85f * -height + offsetY, 0), _self.position + new Vector3(0, 0.85f * -height + offsetY, 0) + 1.1f * width * (Vector3)moveDir);
        }
    }

    void SwitchAnimation()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ani.SetBool("idle", false);
            ani.SetBool("walkx", true);
            ani.SetBool("walkup", false);
            ani.SetBool("walkdown", false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ani.SetBool("walkx", true);
            ani.SetBool("idle", false);
            ani.SetBool("walkup", false);
            ani.SetBool("walkdown", false);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            ani.SetBool("walkx", false);
            ani.SetBool("walkup", true);
            ani.SetBool("idle", false);
            ani.SetBool("walkdown", false);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ani.SetBool("walkx", false);
            ani.SetBool("walkdown", true);
            ani.SetBool("walkup", false);
            ani.SetBool("idle", false);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            ani.SetBool("walkx", false);
            ani.SetBool("walkup", false);
            ani.SetBool("idle", true);
            ani.SetBool("walkdown", false);
        }

        float dispb = 1000f;
        Transform box = null;

        for (int i = 0; i < boxTranform.Count; i++)
        {
            float mindis = (player.position - boxTranform[i].position).sqrMagnitude;
            if (mindis < dispb)
            {
                dispb = mindis;
                box = boxTranform[i];
            }
        }


        if (dispb < disY && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && player.transform.position.y < box.transform.position.y && Mathf.Abs(player.position.x - box.position.x) < 1.5)
        {
            ani.SetBool("pushup", true);

        }
        else
        {
            ani.SetBool("pushup", false);
        }

        if (dispb < disX && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Mathf.Abs(player.transform.position.y - box.transform.position.y) < 1.8 && player.transform.position.x - box.transform.position.x < -1)
        {
            ani.SetBool("pushx", true);

        }
        else
        {
            ani.SetBool("pushx", false);
        }

        if (dispb < disY && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && player.transform.position.y > box.transform.position.y + 1.8 && Mathf.Abs(player.position.x - box.position.x) < 1.5)
        {
            ani.SetBool("pushdown", true);
        }

        else
        {
            ani.SetBool("pushdown", false);
        }

        if (dispb < 4.3 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Mathf.Abs(player.transform.position.y - box.transform.position.y) < 1.6 && player.transform.position.x - box.transform.position.x > 1.5)
        {
            ani.SetBool("pushl", true);
        }
        else
        {
            ani.SetBool("pushl", false);
        }
    }
}
