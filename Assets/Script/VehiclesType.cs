using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesType : MonoBehaviour
{
    [System.Serializable]
    public struct Vehicles
    {
        public GameObject prefab;
        public float speed;
    }

    public Vehicles[] vehicles;
    public static VehiclesType instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject getPrefab(int index)
    {
        return vehicles[index].prefab;
    }

    public float getSpeed(int index)
    {
        return vehicles[index].speed;
    }

}
