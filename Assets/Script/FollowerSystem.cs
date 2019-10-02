using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowerSystem : MonoBehaviour
{
    [Header("instance")]
    public static FollowerSystem instance;

    [Header("follower")]
    public Vector3[] followerSteps;
    public Transform[] follower;
    public int followerNumber = 0;//跟隨人數
    public int followerAmount = 0;//總跟隨人數
    private Transform floder;

    [Header("GhostMaskPrefab")]
    public bool isGhostTime;
    public GameObject ghostMaskPrefab;
    public GameObject[] ghostMask;

    public int times = 0;

    private GameObject player;
    private string playerTag = "Player";

    void Awake()
    {
        if (instance != null) Debug.Log("There are already have FollowerSystem");
        else instance = this;
    }

    void Start()
    {
        floder = transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
        followerSteps = new Vector3[30];
    }

    void Update()
    {
        if (followerNumber <= 0) return;
        if (follower.Length != followerNumber) CheckFollowerNumber();
        
    }

    //store player steps
    public void StoreVecter(Vector3 playerPos)
    {
        if (times > followerSteps.Length -1) times = followerSteps.Length - 1;

        for (int i = times; i > 0; i--)
        {
            followerSteps[i] = followerSteps[i - 1];
        }

        followerSteps[0] = playerPos;
        times++;

    }

    public void AddFollowers()
    {
        followerNumber++;
        followerAmount++;

        if (isGhostTime)
        {
            int i = followerNumber - 1;
            InstantiateMask(i);
        }
    }

    public void RemoveFollowers() {
        followerNumber--;
        CheckFollowerNumber();
    }

    public void CheckFollowerNumber()
    {
        follower = new Transform[floder.childCount];
        ghostMask = new GameObject[floder.childCount];
        for (int i = 0; i < floder.childCount; i++)
        {
            follower[i] = floder.GetChild(i);

            int orgIndex = follower[i].GetComponent<FollowerMovement>().followerIndex;
            if (orgIndex != i + 1) follower[i].GetComponent<FollowerMovement>().followerIndex = i + 1;
        }
    } //重新排列follower的序號

    public Vector3 getNextTransform(int followNumber) //follower get target to move
    {
        return followerSteps[followNumber];
    }

    public int getFollowerIndex() //follower get index
    {
        return followerNumber;
    }

    public void WearGhostMask()
    {
        isGhostTime = true;
        if (floder.childCount == 0) return;

        ghostMask = new GameObject[floder.childCount];

        for (int i = 0; i < floder.childCount; i++)
        {
            InstantiateMask(i);
        }
    }

    public void InstantiateMask(int i)
    {
        CheckFollowerNumber();
        Vector3 pos = new Vector3(follower[i].position.x, follower[i].position.y + 1f, follower[i].position.z);
        ghostMask[i] = Instantiate(ghostMaskPrefab, pos, Quaternion.identity);
        ghostMask[i].transform.Rotate(-90f, -90f, 0);
        ghostMask[i].transform.SetParent(follower[i].transform);
    }


    public GameObject[] storeMask()
    {
        int k = ghostMask.Length;
        int j = floder.childCount;
        GameObject[] newMask = new GameObject[j];
        for (int i = 0; i < k; i++)
        {
            newMask[i] = ghostMask[i];

        }
        Debug.Log("ghostMask : " + ghostMask);
        return newMask;
    }

    public void takeOffMask()
    {
        isGhostTime = false;
        for (int i = 0; i < floder.childCount; i++)
        {
            Transform transform = follower[i].transform;
            //transform.GetComponent<FollowerMovement>().endGhostTime();
            try{
                ghostMask[i] = transform.GetChild(1).gameObject;
            }
            catch(Exception e)
            {
                Debug.LogException(e, this);
            }

            Destroy(ghostMask[i]);
        }
    }

}
