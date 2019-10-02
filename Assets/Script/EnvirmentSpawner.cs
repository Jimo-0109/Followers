using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvirmentSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject[] roads;
    public GameObject plane;
    public GameObject ghostMask;

    [Header("PlayerPosition")]
    private Transform player;
    private bool CreateNewPlane = false;
    public float dirZ;

    [Header("CurrentEnvirment")]
    public GameObject[] planes;
    private Vector3 planeBetweenPlane;
    public int groundAmount = 3;
    bool isDropItem = false;

    void Start()
    {
        planeBetweenPlane = new Vector3(0, 0, 50f); //兩片地板的距離
    }

    void Update()
    {
        planes = GameObject.FindGameObjectsWithTag("Ground"); //讀取現在有幾片地板
        if (groundAmount % 5 == 0 && !isDropItem) CreateItem(); //生產道具

    }

    public void CreateNewGround()
    {
        if (CreateNewPlane) return;
        GameObject newPlane = Instantiate(plane, Vector3.zero + planeBetweenPlane * groundAmount, Quaternion.identity);
        newPlane.transform.SetParent(transform);
        CreateNewPlane = true;

        int runtime = 0;
        int emptyTime = 0;

        Vector3[] Pos = new Vector3[10];
        for (int i = 0; i < 10; i++)
        {
            Vector3 vector3 = new Vector3(0, 0, -22.5f) + new Vector3(0, 0, i * 5);
            Pos[i] = newPlane.transform.position + vector3;

            int index = Random.Range(0, 4); //0123
            if (index >= 0 && index < 3 ) 
            {
                GameObject newRoad = Instantiate(roads[index], Pos[i], Quaternion.identity);
                newRoad.transform.SetParent(newPlane.transform);
            }
            else if(index == 3 && emptyTime <= 3) 
            {
                emptyTime++;
            }
            else if(index == 3 && emptyTime > 3)
            {
                index = Random.Range(0, 3);
                GameObject newRoad = Instantiate(roads[index], Pos[i], Quaternion.identity);
                newRoad.transform.SetParent(newPlane.transform);
            }
            runtime++;
        }
        groundAmount++;
        isDropItem = false;
    }

    public void DestroyGround()
    {
        Destroy(planes[0]);
        CreateNewPlane = false;
    }

    void CreateItem()
    {
        Vector3 pos = planes[planes.Length - 1].transform.position;
        pos += new Vector3(Random.Range(0, 8), 1f, Random.Range(0, 10));

        GameObject item = Instantiate(ghostMask, pos, Quaternion.identity);
        

        isDropItem = true;
    }

    public Vector3 GetPlanePosition(int index)
    {
        return planes[index].transform.position;
    }

    public int GetPlaneNumber()
    {
        return groundAmount;
    }

    //void CreateBoards()
    //{
    //    Vector3 pos = new Vector3(planes[0].transform.position.x, planes[0].transform.position.y, planes[0].transform.position.z - 25f);
    //    GameObject bd = Instantiate(board, pos, Quaternion.identity);

    //    Vector3 TrPos = new Vector3(planes[0].transform.position.x, planes[0].transform.position.y, planes[0].transform.position.z + 25f);
    //    GameObject tr = Instantiate(trigger, TrPos, Quaternion.identity);

    //    bd.transform.SetParent(planes[0].transform);
    //    tr.transform.SetParent(planes[0].transform);
    //}
}
