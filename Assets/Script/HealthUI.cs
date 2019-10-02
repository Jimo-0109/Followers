using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider slider;
    public Color healthColor;
    public Color normalColor;
    public Color AlertColor;
    public Image fill;
    public int MaxHP;
    public int Hp;
    

    private void Start()
    {
        Hp = MaxHP = PlayerController.HP = 3;
        fill.color = healthColor;
    }

    void Update()
    {
        if (PlayerController.HP == Hp) return;
        else HealthCircle();
    }

    void HealthCircle()
    {
        Debug.Log("PlayerController.HP : " + PlayerController.HP);

        int i = Hp - 1;
        switch (i)
        {
            case 3:
                fill.color = healthColor;
                break;
            case 2:
                fill.color = normalColor;
                break;
            case 1:
                fill.color = AlertColor;
                break;
        }

        StartCoroutine(DecreaseCircle(Hp));
        Hp = PlayerController.HP;
    }

    IEnumerator DecreaseCircle(int Hp)
    {
        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime;

            slider.value = Hp - t;
            yield return 0;
        }
    }


}
