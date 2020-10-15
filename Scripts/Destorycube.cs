using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destorycube : MonoBehaviour
{
    public float Destroytime;
    void Start()
    {
        Destroy(gameObject, Destroytime);
    }

    
}
