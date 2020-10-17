using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestory : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    
}
