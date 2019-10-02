using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{
    public GameObject followerPrefab;
    Transform[] InsPoint;
    int number;

    void Start()
    {
        GetInsPoints();
        InsFollower();
    }


    private void GetInsPoints()
    {
        InsPoint = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            InsPoint[i] = transform.GetChild(i);
        }
    }

    private void InsFollower()
    {
        number = Random.Range(1, 4);
        for (int i = 0; i < number; i++)
        {
            Instantiate(followerPrefab, InsPoint[i].position, Quaternion.identity);
        }
    }
}
