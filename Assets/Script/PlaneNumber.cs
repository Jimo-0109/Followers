using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneNumber : MonoBehaviour
{
    public Text text;
    private EnvirmentSpawner envirmentSpawner;
    public int planeNumber = -1;

    private void Start()
    {
        envirmentSpawner = transform.parent.parent.gameObject.GetComponent<EnvirmentSpawner>();

        planeNumber = envirmentSpawner.GetPlaneNumber();
        text.text = planeNumber.ToString();
}
}
