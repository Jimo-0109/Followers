using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform Player;

    float smoothTime = 1f;
    float zVelocity = 0.0f;

    private float zOffset = -12.5f;
    public Vector3 cameraPosition = Vector3.zero;
    public bool focus = false;
    public float time;
    public static float PosY = 30f;
 
    void Update()
    {

        if (!focus)
        {           
            float newPosition = Mathf.SmoothDamp(transform.position.z, Player.position.z + zOffset, ref zVelocity, smoothTime);
            transform.position = new Vector3(transform.position.x, PosY, newPosition);
            time = 0;
            
        }
        else
        {
            focusPlayer();
            time += Time.deltaTime;
            if (time > 0.8f) endFocus();
        }

    }

    public void focusPlayer()
    {
        if(cameraPosition == Vector3.zero) cameraPosition = transform.position;
        focus = true;             
        Vector3 target = new Vector3(transform.position.x, transform.position.y - 6f, transform.position.z + 4);
        Vector3 velocity = Vector3.zero;
        float smoothTime = 0.2f;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
    public void endFocus()
    {
        focus = false;
        Vector3 velocity = Vector3.zero;
        float smoothTime = 0.01f;

        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothTime);

        cameraPosition = Vector3.zero;
    }
}
