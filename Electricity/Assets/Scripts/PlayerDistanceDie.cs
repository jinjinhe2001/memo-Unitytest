using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerDistanceDie : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject eleF;         //用来检测是否两个物体是否连接
    public GameObject LevelLoader;

    public string scenename;
    
    private bool Check_ifWeaking;    //检测是否正在变虚
    private float die_time;         //死亡时间
    private float ColorAlpha;       //物体透明度

    private void Start()
    {
        ColorAlpha = 1f;
        die_time = 3f;
    }
    void Update()
    {
        if(eleF.GetComponent<ElectricField>().if_path)
            Check_ifWeaking = false;
        else
            Check_ifWeaking = true;
        if (Check_ifWeaking)
        {
            ColorAlpha -= 1f / die_time * Time.deltaTime;
            player1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            for (int i = 0; i < player1.transform.childCount; i++)
            {
                player1.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            }
            player2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            for (int i = 0; i < player2.transform.childCount; i++)
            {
                player2.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            }
        }
        else
        {
            ColorAlpha = 1f;
            player1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            for (int i = 0; i < player1.transform.childCount; i++)
            {
                player1.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            }
            player2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            for (int i = 0; i < player2.transform.childCount; i++)
            {
                player2.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, ColorAlpha);
            }
        }

        if (ColorAlpha < 0.01f)
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
