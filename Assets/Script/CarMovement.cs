using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public int index;

    private Vector3 target;

    CarSpawner carSpawner;

    public bool canMove = true;

    void Update()
    {
        float dir = transform.position.x - target.x;       
        if (Mathf.Abs( dir )< 2f) Destroy();

        if(canMove) MoveForward();
    }

    public void SetTarget(Vector3 endPoint)
    {
        target = endPoint;
    }

    public void GetSpawner(CarSpawner _carSpawner)
    {
        carSpawner = _carSpawner;
    }

    void Destroy()
    {
        carSpawner.CarOnRoadDecrease();
        Destroy(gameObject);
    }

    void MoveForward()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, target, VehiclesType.instance.getSpeed(index) * Time.deltaTime);
    }

    public void Stop()
    {
        canMove = false;
        
    }
}
