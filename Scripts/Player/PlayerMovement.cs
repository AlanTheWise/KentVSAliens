using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5, sprintSpeed = 8;
    [SerializeField] float jumpPower = 9;

    [SerializeField] Image staminaFillImg, sprintLines;
    [SerializeField] float staminaRegenSpeed = 0.01f, staminaUseSpeed = 0.015f;
    [SerializeField] Camera cam;
    [SerializeField] AudioSource lowStaminaSound, jumpSound;

    Rigidbody rb;
    Collider col;
    float currentSpeed;
    [HideInInspector] public bool canSprint = true, isSprinting, inputRunUp = true;

    public static PlayerMovement sharedInstance;

    private void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        StartCoroutine("ManageStamina");
    }

    void Update()
    {
        if (UIController.sharedInstance.pausePanelActive || UIController.sharedInstance.deadMenuActive) return;

        MovePlayer();

        if(IsGrounded() && Input.GetButtonDown("Jump")){
            jumpSound.Play();
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        if(Input.GetButton("Run") && canSprint && (rb.velocity != Vector3.zero)){
            currentSpeed = sprintSpeed;

            sprintLines.enabled = true;

            if (cam.fieldOfView < 85){
                cam.fieldOfView += 3;
            }
            
            isSprinting = true;
            inputRunUp = false;
        } else {
            if (cam.fieldOfView > 75){
                cam.fieldOfView -= 3;
            }
            sprintLines.enabled = false;
            currentSpeed = walkSpeed;
            isSprinting = false;
        }

        if(Input.GetButtonUp("Run")){
            inputRunUp = true;
        }
    }

    public string GetStandingLayerName(){
        string layerName = "";
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, col.bounds.extents.y + 2f);
        if (hit.collider != null){
            layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
        }
        return layerName;
    }

    void MovePlayer(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;

        if(horizontal != 0 || vertical != 0){
            Vector3 direction = (transform.forward * vertical + transform.right * horizontal).normalized;
            velocity = direction * currentSpeed;
        }
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        cam.GetComponent<Animator>().SetFloat("playerSpeed", velocity.magnitude);
    }

    bool IsGrounded(){
        return Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), 0.4f, LayerMask.GetMask("Ground", "Box"));
    }

    IEnumerator ManageStamina(){
        while (true){
            if (Mathf.Approximately(staminaFillImg.fillAmount, 0f)){
                if (!lowStaminaSound.isPlaying) lowStaminaSound.Play();

                canSprint = false;
                if (inputRunUp){
                    canSprint = true;
                }
            } 

            if (isSprinting){
                staminaFillImg.fillAmount -= staminaUseSpeed;
            } else if (canSprint){
                staminaFillImg.fillAmount += staminaRegenSpeed;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
