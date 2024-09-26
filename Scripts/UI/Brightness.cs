using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    [SerializeField] Slider brightnessSlider;

    [SerializeField] PostProcessProfile brightness;
    [SerializeField] PostProcessLayer layer;

    AutoExposure exposure;

    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        AdjustBrightness(brightnessSlider.value);
    }

    public void AdjustBrightness(float value){
        if(value != 0){
            exposure.keyValue.value = value;
        } else{
            exposure.keyValue.value = 0.5f;
        }
    }
}
