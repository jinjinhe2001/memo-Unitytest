using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destorysound : MonoBehaviour
{
    void Start()
    {
        GameObject.Destroy(gameObject, 1);
    }

}
