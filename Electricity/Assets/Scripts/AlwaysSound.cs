using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysSound : MonoBehaviour
{
    public GameObject sonud;
    GameObject obj;
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("sound");
        if (obj == null)
            GameObject.Instantiate(sonud);
    }
}
