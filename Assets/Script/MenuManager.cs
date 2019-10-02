using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioSource carHorn;
    public FadeUI fadeUI;
    public GameObject car;
    public Animator animator;
    public Button startButton;
    public Button quitButton;
    private float time;
    private bool isPlayCarhorn = false;
    private bool setAnimation = false;

    public string sceneName = "Stage";

    private void Start()
    {
        initButton();
    }

    private void Update()
    {
        if (isPlayCarhorn && setAnimation) return;

        time += Time.deltaTime;
        if (!isPlayCarhorn)
        {
            if (time >= 2f) PlayCarHorn();
        }
        if (!setAnimation)
        {
            if (time >= 2.5f)
            {
                animator.SetBool("carHornEnd", true);
                setAnimation = true;
                ButtonActive();
            }

        }
    }

    public void PlayCarHorn()
    {
        carHorn.PlayOneShot(carHorn.clip);
        isPlayCarhorn = true;      
    }

    //button init
    public void initButton()
    {
        startButton.interactable = false;
        quitButton.interactable = false;
    }

    public void ButtonActive()
    {
        startButton.interactable = true;
        quitButton.interactable = true;
    }


    //for button click
    public void StartButton()
    {
        Destroy(car);
        fadeUI.FadeTO(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
