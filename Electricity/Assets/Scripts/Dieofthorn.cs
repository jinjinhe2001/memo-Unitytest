using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dieofthorn : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject LevelLoader;

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
        if (Mathf.Abs(dis1) < 0.7f || Mathf.Abs(dis2) < 0.7f)
        {
            StartCoroutine(ChangeScene(scenename));
        }
    }
    private IEnumerator ChangeScene(string name)
    {
        LevelLoader.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        LevelLoader.transform.GetChild(0).GetComponent<Animator>().SetBool("start", false);
        LevelLoader.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(name);

    }
}
