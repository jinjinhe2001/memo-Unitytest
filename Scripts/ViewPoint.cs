using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    void Update()
    {
        gameObject.GetComponent<Transform>().position = (player1.GetComponent<Transform>().position + player2.GetComponent<Transform>().position) / 2;
    }
}
