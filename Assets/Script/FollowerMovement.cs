using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [Header("Follower Attribute")]
    private Transform floder;
    public int followerIndex = -1;
    public bool isFollow = false;
    public bool isChase = false;
    public bool isDeath = false;
    private BoxCollider boxCollider;
    private FollowerSystem followerSystem;

    [Header("Animation")]
    public Animator animator;

    [Header("player Position")]
    private Transform player;
    private string playerTag = "Player";
    private Vector3 playerPos;

    [Header("Movement")]
    public Vector3 target;
    private float speed = 5f;
    public Rigidbody rb;
    public float forceX;
    public float forceY;

    [Header("Effect")]
    public GameObject deathPrefab;
    public GameObject heartShakingPrefab;
    private GameObject heartShakingEffect;
    private float offectX = 0f;
    private float offectY = 2.95f;
    private float offectZ = 0.5f;
    AudioSource audioSoure;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        followerSystem = FollowerSystem.instance;
        floder = GameObject.FindGameObjectWithTag("Follower").transform;
        audioSoure = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        LockRotation();
    }

    void Update()
    {
        //follower dead
        if (isDeath)
        {
            return; 
        }

        //player dont move
        if (Vector3.Distance(playerPos, player.position) < 0.2f) return;

        //move 
        if (!isFollow && !isChase)
        {
            float dir = transform.position.z - player.position.z;
            if (Mathf.Abs(dir) < 1.2f) Chasing();
        }
        else if (!isFollow && isChase)
        {
            BeFollower();
            Move();
        }

        playerPos = player.position;
    }

    void FixedUpdate()
    {
        if (isDeath) return;
        if (FollowerSystem.instance.isGhostTime) isGhostTime();
        else endGhostTime();
        if (isFollow) Move();
    }

    void Chasing()
    {         
        Vector3 pos = new Vector3(transform.position.x + offectX, transform.position.y + offectY, transform.position.z+ offectZ);
        heartShakingEffect = Instantiate(heartShakingPrefab, pos, Quaternion.identity);
        heartShakingEffect.transform.parent = transform;

        isChase = true;
    }

    void BeFollower()
    {
        transform.SetParent(floder);
        followerSystem.AddFollowers();
        isFollow = true;
        followerIndex = followerSystem.getFollowerIndex();
       
        Destroy(heartShakingEffect);

    }

    void Move()
    {
        target = followerSystem.getNextTransform(followerIndex);

        //dont pass target
        if (target == Vector3.zero) return;

        //target dont change
        if (Vector3.Distance(transform.position, target) < 0.02f) return;
        else
        {
            //move
            animator.SetBool("isMoving", true);

            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }



    }

    public void EndMoving()
    {
        animator.SetBool("isMoving", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicles"))
        {
            if (!isDeath) 
            {
                UnlockRotation();
                audioSoure.PlayOneShot(audioSoure.clip); // play sound
                CarAccident();
            }
           
        }

        if (!isDeath) return;
        if (collision.gameObject.CompareTag("Ground") && isDeath)
        {
            Falldown();
        }
    }

    void CarAccident()
    {
        if (isDeath) return;
        isDeath = true;

        followerSystem.RemoveFollowers(); //decrease follower

        animator.SetBool("isDeaded", true);
        Hit();
    }

    void Hit()
    {      
        rb.AddForce(forceX, forceY, 0);
        Destroy(this.gameObject, 4f);
    }
    private void Falldown()
    {
        //transform.Rotate(0, 0, 90f);

        Destroy(this.gameObject,0.2f);

        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        GameObject deathEffect = Instantiate(deathPrefab, position, Quaternion.identity);
        Destroy(deathEffect, 2f);
    }

    public void LockRotation()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void UnlockRotation()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void isGhostTime()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        boxCollider.enabled = false;
    }

    public void endGhostTime()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        boxCollider.enabled = true;
    }
}
