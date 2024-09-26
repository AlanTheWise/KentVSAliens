using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 4;
    [SerializeField] float minClamp = -90, maxClamp = 90;
    [SerializeField] Slider mouseSensitivitySlider;
    
    Transform player;
    float rotationY;

    void Start()
    {
        player = transform.parent;
    }

    void Update()
    {
        if (UIController.sharedInstance.pausePanelActive || UIController.sharedInstance.deadMenuActive) return;
        // Mirar a los lados
        player.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);

        // Mirar arriba y abajo
        rotationY += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotationY = Mathf.Clamp(rotationY, minClamp, maxClamp);
        
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }

    public void changeSensitivity(){
        lookSensitivity = mouseSensitivitySlider.value;                
    }
}
