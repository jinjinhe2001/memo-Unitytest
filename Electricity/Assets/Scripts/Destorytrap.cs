using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Destorytrap : MonoBehaviour
{
    public float Destroytime;

    private float ColorAlpha;       //物体透明度
    private bool Check_ifsmash;
    void Start()
    {
        ColorAlpha = 1f;
        Invoke("smash", Destroytime);
        Check_ifsmash = false;
    }
    void smash()
    {
        Check_ifsmash = true;
        Invoke("CloseBox", 0.1f);
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 3;
            //gameObject.transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        }
        Invoke("Des", 1);
    }
    void Des()
    {
        GameObject.Destroy(gameObject, 0.5f);
    }
    void CloseBox()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void Update()
    {
        if (Check_ifsmash)
        {
            ColorAlpha -= 2f * Time.deltaTime;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            }
        }   
    }
}
