using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VR;

public class Control : MonoBehaviour
{
    Animator ani;
    bool FaceLeft = true;
    Rigidbody2D rig;
    public bool Ground = false;
    public GameObject ele;
    int Getcondition = 0;   //0代表未进入触发区，1代表进入触发区，-1代表正在Get 
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
    
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.name == "ground"||)
        {
            Ground = true;
            ani.SetBool("jumping", false);
        }
    }


    void check_Get()
    {
        float sqrLenght = (ele.transform.localPosition - transform.position).sqrMagnitude;
        if (sqrLenght < 9 && Getcondition != -1)//因为sqrLenght是平方，所以对比值也需要平方
        {
            Getcondition = 1;
        }
        else if (Getcondition != -1)
            Getcondition = 0;
    }
    // Update is called once per frame
    void Update()
    {
        int skip = 0;
        check_Get();
        float h = Input.GetAxisRaw("Horizontal");
        ani.SetFloat("speed", Mathf.Abs(h));
        
        if(FaceLeft && h > 0)
        {
            flip();
        }
        if (!FaceLeft && h < 0)
        {
            flip();
        }
        transform.Translate(Vector2.right * Time.deltaTime * 3 * h);
        if (Ground && Input.GetKeyDown(KeyCode.W))
        {
            rig.AddForce(Vector2.up * 250);
            Ground = false;
            ani.SetBool("jumping", true);
        }
        if (Input.GetKeyDown(KeyCode.S) && Getcondition == 1) 
        {
            Getcondition = -1;
            ele.GetComponent<BoxCollider2D>().enabled = false;

            ele.GetComponent<Rigidbody2D>().gravityScale = 0;
            skip = 1;
        }
        
        if (Getcondition == -1)
        {
            if (FaceLeft)
                ele.transform.localPosition = Vector3.MoveTowards(ele.transform.localPosition, new Vector3(transform.position.x + 1, transform.position.y + 1, 0f), 10*Time.deltaTime);
            else
                ele.transform.localPosition = Vector3.MoveTowards(ele.transform.localPosition, new Vector3(transform.position.x - 1, transform.position.y + 1, 0f), 10*Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.S) && Getcondition == -1 && skip == 0) 
        {
            Getcondition = 1;
            ele.GetComponent<BoxCollider2D>().enabled = true;

            ele.GetComponent<Rigidbody2D>().gravityScale = 1;
            
        }
        
        

    }
    
}
