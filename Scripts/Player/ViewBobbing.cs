using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBobbing : MonoBehaviour
{
    [SerializeField] float effectIntensity, effectIntensityX, effectSpeed;

    private float effectSpeedWalk, effectSpeedRun;

    PositionFollower followerInstance;
    Vector3 originalOffset;
    float sinTime;

    void Start()
    {
        followerInstance = GetComponent<PositionFollower>();
        originalOffset = followerInstance.offset;

        effectSpeedWalk = effectSpeed;
        effectSpeedRun = effectSpeed * 2f;
    }

    void Update()
    {
        if (PlayerMovement.sharedInstance.isSprinting){
            effectSpeed = effectSpeedRun;
        } else{
            effectSpeed = effectSpeedWalk;
        }

        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if(inputVector.magnitude > 0f){
            sinTime += Time.deltaTime * effectSpeed; 
        } else {
            sinTime = 0f;
        }
        
        float sinAmountY = -Mathf.Abs(effectIntensity * Mathf.Sin(sinTime));
        Vector3 sinAmountX = followerInstance.transform.right * effectIntensity * Mathf.Cos(sinTime) * effectIntensityX;

        followerInstance.offset = new Vector3{
            x = originalOffset.x,
            y = originalOffset.y + sinAmountY,
            z = originalOffset.z
        };

        followerInstance.offset += sinAmountX;
    }
}
