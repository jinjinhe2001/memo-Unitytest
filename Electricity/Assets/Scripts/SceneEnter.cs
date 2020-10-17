using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnter : MonoBehaviour
{   
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("start",true);
        Invoke("Disable", 1f);
    }
    void Disable()
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("start",false);
        gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }
}
