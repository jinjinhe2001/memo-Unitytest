using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Beltmove : MonoBehaviour
{
    public float Speed;
    public int Belt_direction;
    void OnCollisionStay2D(Collision2D col)
    {
        col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, col.gameObject.transform.position + new Vector3(Belt_direction, 0, 0), Speed * Time.deltaTime);
    }
}
