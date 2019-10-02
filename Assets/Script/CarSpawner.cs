using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private bool firstRound = false;
    public bool isLeftRoad;

    public Transform instatiatePos;
    public Transform endPoint;
    public CarMovement[] carMovement;

    private int CarOnRoad = 0;
    private float time = 0;
    private float timeBetweenNext = 0;

    void Start()
    {
        StartCoroutine(CreateVehicles());
    }

    void Update()
    {
        time += Time.deltaTime;
        if (firstRound && time >= timeBetweenNext) 
        {         
           Instantiate();
            timeBetweenNext = Random.Range(2f, 5f);
            time = 0;
        }
    }

    IEnumerator CreateVehicles()
    {
        float waitTime = Random.Range(1f, 5f);
        while (CarOnRoad < 2)
        {
            Instantiate();
            yield return new WaitForSeconds(waitTime);
        }
        firstRound = true;
    }

    private void Instantiate()
    {
        Quaternion quaternion;
        if (isLeftRoad)
            quaternion = Quaternion.Euler(0, 90, 0);
        else
            quaternion = Quaternion.Euler(0, 270, 0);

        int RandomIndex = Random.Range(0, 7);

        GameObject Car = Instantiate(VehiclesType.instance.getPrefab(RandomIndex), instatiatePos.position, quaternion);
        Car.transform.parent = this.transform;

        CarMovement carMovement = Car.GetComponent<CarMovement>();
        carMovement.SetTarget(endPoint.position);
        carMovement.GetSpawner(this);

        CarOnRoad++;
    }

    public void CarOnRoadDecrease()
    {
        CarOnRoad--;
    }

}
