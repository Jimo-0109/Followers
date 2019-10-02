using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Attribute")]
    private Transform target;
    private float stepAmount = 2;
    private float speed = 0.001f;
    public static bool isPlayerMove = false;
    private float timeBetweenStep = 0.1f;
    private float time;
    private Vector3 playerPos;

    [Header("Save walk position")]
    private FollowerSystem followerSystem;

    [Header("Get Plane Position")]
    public EnvirmentSpawner envirmentSpawner;

    void Start()
    {
        followerSystem = FollowerSystem.instance;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time < timeBetweenStep) return;

        playerPos = transform.position;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {            
            SetTarget(1); // moveRight
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {        
            SetTarget(2); // moveLeft
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetTarget(3); // move up
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetTarget(4); // move down
        }

        if (target == null) {
            isPlayerMove = false;
            return;
        }
        else
        {
            Vector3 dir = target.transform.position - playerPos;
            transform.LookAt(target, dir);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            

            target = null;
            time = 0;
        }

        float PosZ = envirmentSpawner.GetPlanePosition(0).z;
        if (playerPos.z > PosZ + 25f) envirmentSpawner.CreateNewGround();
        if (playerPos.z > PosZ + 50f) envirmentSpawner.DestroyGround();

        
    }

    public void SetTarget(int type)
    {
        if (time < timeBetweenStep) return;
        if (!CheckBoarder(type)) return;
        target = transform;
        isPlayerMove = true;
        switch (type)
        {
            case 1:
                target.position += new Vector3(stepAmount, 0, 0);
                break;
            case 2:
                target.position -= new Vector3(stepAmount, 0, 0);
                break;
            case 3:
                target.position += new Vector3(0, 0, stepAmount);
                break;
            case 4:
                target.position -= new Vector3(0, 0, stepAmount);
                break;
        }
        followerSystem.StoreVecter(target.position);
    }

    public bool CheckBoarder(int type)
    {
        if (type == 1)
        {
            if (transform.position.x >= 8) return false;
            else return true;
        }
        else if (type == 2)
        {
            if (transform.position.x <= -8) return false;
            else return true;
        }
        else if (type == 3)
        {
            return true;
        }
        else 
        {
            float PosZ = (envirmentSpawner.groundAmount - 3) * 50 - 10f;
            if (transform.position.z <= PosZ) return false;
            else return true;
        }
        
    }

  
}
