using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesChange : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public string scenename;
    Vector3 pos1;
    Vector3 pos2;
    void Start()
    {
        pos1 = player1.GetComponent<Transform>().position;
        pos2 = player2.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = player1.GetComponent<Transform>().position;
        pos2 = player2.GetComponent<Transform>().position;
        Vector3 pos3 = gameObject.GetComponent<Transform>().position;
        float dis1 = Vector3.Distance(pos1, pos3);
        float dis2 = Vector3.Distance(pos2, pos3);
        if (Mathf.Abs(dis1) < 1 && Mathf.Abs(dis2) < 1)
        {
            player1.GetComponent<BoxCollider2D>().enabled = false;
            player1.GetComponent<Rigidbody2D>().gravityScale = 0;
            player2.GetComponent<BoxCollider2D>().enabled = false;
            player2.GetComponent<Rigidbody2D>().gravityScale = 0;

            player1.transform.position= Vector3.MoveTowards(player1.transform.position, new Vector3(transform.position.x, transform.position.y, 0f), 1.5f * Time.deltaTime);
            player2.transform.position = Vector3.MoveTowards(player2.transform.position, new Vector3(transform.position.x, transform.position.y, 0f), 1.5f * Time.deltaTime);
            if(Mathf.Abs(dis1) <0.1f && Mathf.Abs(dis2) < 0.1f)
            ChangeScene(scenename);
        }
    }
    void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
