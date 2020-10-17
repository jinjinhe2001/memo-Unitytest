using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public GameObject door;
    private bool Check_swith;

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

            //door.transform.GetChild(0).GetComponent<BoxCollider2D>().size= new Vector2(door.transform.GetChild(0).GetComponent<BoxCollider2D>().size.x, door.transform.GetChild(0).GetComponent<BoxCollider2D>().size.y - 0.35f);
            //door.transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(door.transform.GetChild(0).GetComponent<BoxCollider2D>().size.x, door.transform.GetChild(0).GetComponent<BoxCollider2D>().size.y - 0.35f);
            
            Invoke("Check_return", 1f);
        }
    }
    private void Update()
    {
        if (Check_swith)
        {
            door.transform.GetChild(0).localPosition = Vector3.MoveTowards(door.transform.GetChild(0).localPosition, new Vector3(door.transform.GetChild(0).localPosition.x, door.transform.GetChild(0).localPosition.y + 1, 0f), 1 * Time.deltaTime);
            door.transform.GetChild(1).localPosition = Vector3.MoveTowards(door.transform.GetChild(1).localPosition, new Vector3(door.transform.GetChild(1).localPosition.x, door.transform.GetChild(1).localPosition.y - 1, 0f), 1 * Time.deltaTime);
        }
    }
    void Check_return()
    {
        GameObject.Destroy(door);
        Check_swith = false;
    }
}
