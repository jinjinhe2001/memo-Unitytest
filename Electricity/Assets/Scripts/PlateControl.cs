using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateControl : MonoBehaviour
{
    public float Speed;
    public float Staytime;
    public float Move_dis;
    private Vector3 start;
    private Vector3 end;
    private int dir;   //1->up 0->stay -1->down
    void Start()
    {
        start = gameObject.transform.localPosition;
        end = start + new Vector3(0f, Move_dis, 0f);
        dir = 1;
    }
    private void Update()
    {
        if (dir==1)
        {
            Upgo();
            if(Vector3.Distance(gameObject.transform.localPosition, end) < 0.1f)
            {
                dir = 0;
                Invoke("Change1", Staytime);
            }
        }
        if (dir == -1)
        {
            Downgo();
            if (Vector3.Distance(gameObject.transform.localPosition, start) < 0.1f)
            {
                dir = 0;
                Invoke("Change2", Staytime);
            }
        }
    }
    void Upgo()
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, end, Speed * Time.deltaTime);
    }
    void Downgo()
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, start, Speed * Time.deltaTime);
    }
    void Change1()
    {
        dir = -1;
    }
    void Change2()
    {
        dir = 1;
    }
}
