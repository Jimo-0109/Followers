using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("UI")]
    public Text scoreUI;

    [Header("score")]
    public static float score = 0;

    [Header("Player")]
    private Transform player;
    private string playerTag = "Player";
    private int followerNumber;

    [Header("Step")]
    public float dirZ;
    public float newDirZ;
    public float maxZ;

    private void Start()
    {
        score = 0;
        scoreUI.text = "0";
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    void Update()
    {
        newDirZ = player.position.z;
        followerNumber = FollowerSystem.instance.followerNumber;

        float stepZ = newDirZ - dirZ; //上一步和下一步的間距差       

        if (stepZ <= 0)//沒動or往回走
        {
            dirZ = newDirZ;
            return; 
        }
        if (newDirZ <= maxZ) //往回走又往前
        {
            dirZ = newDirZ;
            return;
        }

        maxZ = newDirZ;
        score += (stepZ + followerNumber) * (1 + followerNumber / 10);
        score = Mathf.Round(score);
        scoreUI.text = score.ToString();

        dirZ = newDirZ;
    }
}
