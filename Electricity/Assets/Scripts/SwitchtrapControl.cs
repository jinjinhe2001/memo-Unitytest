using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchtrapControl : MonoBehaviour
{
    public bool Check_swith;

    private void Start()
    {
        Check_swith = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ele" && !Check_swith)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.transform.GetComponent<BoxCollider2D>().size.x, gameObject.transform.GetComponent<BoxCollider2D>().size.y - 0.3f);
            gameObject.transform.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.transform.GetComponent<BoxCollider2D>().offset.x, gameObject.transform.GetComponent<BoxCollider2D>().offset.y - 0.15f);
            Check_swith = true;
        }
    }

}
