using System.Collections.Generic;
using UnityEngine;
using System;
using DijkstraAlgorithm;

public class ElectricField : MonoBehaviour
{
    public GameObject FlashLine;//导入perfabs:FlashLine
    public GameObject[] conductor;//导体第一个为player1,第二个为player2，其余为在场继电器
    public bool if_path;            //检测是否通路传递给PlayerDistanceDie
    
    private System.Collections.Generic.List<string> pre_path;       //路径变了就重新生产flash
    private GameObject[] DestroyF;   //创建一个全部游戏物件GameObject类型的数组
    void Update()
    {
        var weights = new List<Tuple<string, string, double>>();
        for (int i = 0; i < conductor.Length; i++)
        {
            for (int j = i + 1; j < conductor.Length; j++)
            {
                float dis = Vector3.Distance(conductor[i].transform.position, conductor[j].transform.position);
                if (dis <= 3.2f)
                {
                    weights.Add(new Tuple<string, string, double>(i.ToString(), j.ToString(), dis));
                }
            }
        }

        var dij = new Dijkstra();
        dij.InitWeights(weights);
        var path = dij.Find("0", "1");
        bool equal = false;
        if (path == null || path.Count == 0) 
            if_path = false;
        else
            if_path = true;
        if ((pre_path == null || pre_path.Count == 0) && (path != null && path.Count != 0)) 
            equal = false;
        if ((pre_path != null && pre_path.Count != 0) && (path == null || path.Count == 0)) 
            equal = false;
        if ((pre_path != null && pre_path.Count != 0) && (path != null && path.Count != 0)) 
        {
            if (path.Count == pre_path.Count)
            {
                int i;
                for (i = 0; i < path.Count; i++)
                {
                    if (path[i] != pre_path[i])
                        break;
                }
                if (i == path.Count)
                {
                    equal = true;
                }
            }
        }
        pre_path = path;
        if (!equal)
        {
            DestroyF = GameObject.FindGameObjectsWithTag("flash");
            for (int i = 0; i < DestroyF.Length; i++)  
            {
                GameObject.Destroy(DestroyF[i]);    
            }
            
            int[] path_int = new int[path.Count];
            //check_flash = new bool[path.Count, path.Count];
            for (int i = 0; i < path.Count; i++)
            {
                path_int[i] = Convert.ToInt32(path[i]);
            }
            for (int i = 0; i < path.Count - 1; i++)
            {
                //bool Temp = check_flash[path_int[i],path_int[i + 1]];
                //if (!Temp)
                {
                    GameObject F = GameObject.Instantiate(FlashLine);
                    F.GetComponent<FlashLine>().StartPosition = conductor[path_int[i]].transform;
                    F.GetComponent<FlashLine>().EndPostion = conductor[path_int[i+1]].transform;

                }
            }
        }
    }


}
