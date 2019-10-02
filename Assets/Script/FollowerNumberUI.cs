using UnityEngine;
using UnityEngine.UI;

public class FollowerNumberUI : MonoBehaviour
{
    public Text number;

    void Update()
    {
        number.text = FollowerSystem.instance.followerNumber.ToString();
    }
}
