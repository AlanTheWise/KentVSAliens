using UnityEngine.UI;
using UnityEngine;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] AudioListener audioListener;

    void Update()
    {
        AudioListener.volume = GetComponent<Slider>().value;
    }
}
