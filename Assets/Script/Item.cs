using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("UI")]
    public GameObject itemUi;
    public Slider itemTime;

    [Header("Player")]
    private Transform player;
    private BoxCollider playerBoxCollider;
    public GameObject MaskPrefab;
    private GameObject mask;

    [Header("Item Attribute")]
    public bool ghostMask = false;
    public bool isPicked = false;
    private float ghostTime = 5f;
    public float time;

    private void Start()
    {
        itemUi = GameObject.FindGameObjectWithTag("ItemUI");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBoxCollider = player.transform.GetComponent<BoxCollider>();
    }

    private void Update()
    {      
        if (isPicked) //已撿
        {
            if (ghostMask)
            {
                time += Time.deltaTime;
                itemTime.value = 1 - time / ghostTime;

                if (time > ghostTime)
                {
                    ItemEndTime();
                }
            }
        }
        else //未撿
        {
            if (Vector3.Distance(transform.position, player.position) < 2f) PickUp();
        }
        
    }

    public void PickUp()
    {
        isPicked = true;

        openItemUI();

        GhostMask();
        CloseRender();

        Destroy(gameObject, 20f);
    }

    public void openItemUI()
    {
        for (int i = 0; i < itemUi.transform.childCount; i++)
        {
            itemUi.transform.GetChild(i).gameObject.SetActive(true);
        }
        itemTime = itemUi.transform.GetChild(0).transform.GetComponent<Slider>();
    }

    public void closeItemUI()
    {
        for (int i = 0; i < itemUi.transform.childCount; i++)
        {
            itemUi.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GhostMask()
    {
        ghostMask = true;

        //player get mask
        Vector3 pos = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        mask = Instantiate(MaskPrefab, pos, Quaternion.identity);
        mask.transform.Rotate(-90f, -90f, 0);
        mask.transform.SetParent(player);
        playerBoxCollider.enabled = false;

        //follower get mask
        FollowerSystem.instance.WearGhostMask();
    }

    public void CloseRender()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }        
    }

    public void ItemEndTime()
    {
        Destroy(mask);
        playerBoxCollider.enabled = true;

        FollowerSystem.instance.takeOffMask();

        time = 0;
        ghostMask = false;
        closeItemUI();
    }
}

