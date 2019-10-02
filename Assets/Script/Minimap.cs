using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; //固定y
        newPosition.x = transform.position.x;//固定y

        transform.position = newPosition;
    }
}
