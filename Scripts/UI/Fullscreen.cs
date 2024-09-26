using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    private void Start() {
        Screen.fullScreen = true;
    }
    public void ChangeBrightness(){
        Screen.fullScreen = !Screen.fullScreen;
    }
}
