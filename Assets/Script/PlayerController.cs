using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CameraController cameraController;
    public Vector3 cameraPosition;

    public static int HP = 3;
    private string cars = "Vehicles";
    public float smoothTime = 0.3F;
    bool isCarAccident = false;
    float time;

    AudioSource audioSource;

    private void Start()
    {
        HP = 3;
        audioSource = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        if (isCarAccident)
        {
            time += Time.deltaTime;
            if (time > 0.8f)
            {
                Debug.Log("RunTime");           
                Time.timeScale = 1f;
                isCarAccident = false;
                HP--;
            }
        }
        if (HP <= 0) GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(cars))
        {
            if (!isCarAccident)
            {
                audioSource.PlayOneShot(audioSource.clip);
                cameraController.focusPlayer();
                CarAccident();
                
            }
            collision.transform.GetComponent<CarMovement>().Stop();
            
        }
    }

    void CarAccident()
    {
        Debug.Log("CarAccident  ");
       
        Time.timeScale = 0.5f;   
        isCarAccident = true;
        time = 0;
    }

    void GameOver()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
    }
    
}
