using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMcontrol : MonoBehaviour
{
    Animator ani;
    bool FaceLeft = true;
    Rigidbody2D rig;
    public bool Ground = false;
    public GameObject[] ele;
    public GameObject dust;
    public int Getcondition = 0;   //0代表未进入触发区，1代表进入触发区，-1代表正在Get 
    public int DirectionC;//角色碰撞物体的方向
    public GameObject jump_sound;

    private int min_ele;
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

    }
    void flip()
    {
        Vector3 v = transform.localScale;
        v.x *= -1;
        transform.localScale = v;
        FaceLeft = !FaceLeft;
    }

    public enum directions
    {
        up,//从上往下碰撞
        down,//从下往上
        left,//从左往右
        right,//从右往左
        not//无左右碰撞
    }

    void OnCollisionEnter2D(Collision2D col)
    {   
        ContactPoint2D[] contactPoint = new ContactPoint2D[1];//存储碰撞方向的单位向量
        col.GetContacts(contactPoint);//获取碰撞点
        if (contactPoint[0].normal.x < 0 && contactPoint[0].normal.y == 0)
        {

            DirectionC = (int)directions.left;//从左往右
        }
        else if (contactPoint[0].normal.x > 0 && contactPoint[0].normal.y == 0)
        {
            DirectionC = (int)directions.right;//从右往左

        }
        else if (contactPoint[0].normal.y > 0 && contactPoint[0].normal.x == 0)
        {
            DirectionC = (int)directions.up;//从上往下

        }
        else if (contactPoint[0].normal.y < 0 && contactPoint[0].normal.x == 0)
        {
            DirectionC = (int)directions.down;//从下往上
        }
        if (DirectionC == (int)directions.up)
        {
            Ground = true;
            ani.SetBool("jumping", false);
            if (col.gameObject.tag != "plate")
            {
                GameObject.Instantiate(dust, gameObject.transform.position - new Vector3(0, 0.4f, 1), Quaternion.identity);
            }
        }
        
    }
    void OnCollisionStay2D(Collision2D col)
    {
        ContactPoint2D[] contactPoint = new ContactPoint2D[1];//存储碰撞方向的单位向量
        col.GetContacts(contactPoint);//获取碰撞点

        //开始判定
        if (contactPoint[0].normal.x < 0 && contactPoint[0].normal.y == 0)
        {

            DirectionC = (int)directions.left;//从左往右
        }
        else if (contactPoint[0].normal.x > 0 && contactPoint[0].normal.y == 0)
        {
            DirectionC = (int)directions.right;//从右往左

        }
        else if (contactPoint[0].normal.y > 0 && contactPoint[0].normal.x == 0)
        {
            DirectionC = (int)directions.up;//从上往下

        }
        else if (contactPoint[0].normal.y < 0 && contactPoint[0].normal.x == 0)
        {
            DirectionC = (int)directions.down;//从下往上
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        DirectionC = (int)directions.not;
    }

    void check_Get()
    {
        float dis_min = 4;
        float head_dir = transform.localScale.x;
        for (int i = 0; i < ele.Length; i++)
        {
            bool if_getable = ele[i].GetComponent<BoxCollider2D>().enabled;
            float dis_x = (ele[i].transform.position.x - transform.position.x);
            float dis_y = (ele[i].transform.position.y - transform.position.y);
            if (dis_x < 3 && dis_x >= 0 && head_dir <= 0 && Mathf.Abs(dis_y) < 0.7f && Getcondition != -1 && if_getable)  //因为sqrLenght是平方，所以对比值也需要平方
            {
                Getcondition = 1;
                if (dis_min > dis_x)
                {
                    dis_min = dis_x;
                    min_ele = i;
                }
            }
            else if (dis_x < 0 && dis_x > -3f && head_dir > 0 && Mathf.Abs(dis_y) < 0.7f && Getcondition != -1 && if_getable) 
            {
                Getcondition = 1;
                if (dis_min > Mathf.Abs(dis_x))
                {
                    dis_min = Mathf.Abs(dis_x);
                    min_ele = i;
                }
            }
            else if (Getcondition != -1 && dis_min > 3f) 
                Getcondition = 0;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        int skip = 0;
        check_Get();
        int h = 0;
        if (Input.GetKey(KeyCode.D) && DirectionC != (int)directions.left)
            h = 1;
        else if (Input.GetKey(KeyCode.A) && DirectionC != (int)directions.right) 
            h = -1;
        else
            h = 0;
        if (h == 0)
            ani.SetBool("run", false);
        else
            ani.SetBool("run", true);

        if (FaceLeft && h > 0)
        {
            flip();
        }
        if (!FaceLeft && h < 0)
        {
            flip();
        }
        transform.Translate(Vector2.right * Time.deltaTime * 5.2f * h);
        if (Ground && Input.GetKeyDown(KeyCode.W))
        {
            rig.AddForce(Vector2.up * 560);
            Ground = false;
            ani.SetBool("jumping", true);
            GameObject.Instantiate(jump_sound);
        }
        if (Input.GetKeyDown(KeyCode.S) && Getcondition == 1)
        {
            Getcondition = -1;
            ele[min_ele].GetComponent<BoxCollider2D>().enabled = false;

            ele[min_ele].GetComponent<Rigidbody2D>().gravityScale = 0;
            skip = 1;
        }

        if (Getcondition == -1)
        {
            if (FaceLeft)
                ele[min_ele].transform.localPosition = Vector3.MoveTowards(ele[min_ele].transform.localPosition, new Vector3(transform.position.x - 1, transform.position.y + 1, 0f), 12 * Time.deltaTime);
            else
                ele[min_ele].transform.localPosition = Vector3.MoveTowards(ele[min_ele].transform.localPosition, new Vector3(transform.position.x + 1, transform.position.y + 1, 0f), 12 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.S) && Getcondition == -1 && skip == 0)
        {
            Getcondition = 1;
            ele[min_ele].GetComponent<BoxCollider2D>().enabled = true;

            ele[min_ele].GetComponent<Rigidbody2D>().gravityScale = 1;

        }



    }

}
