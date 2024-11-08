using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private MovementSettings movementSettings = new MovementSettings();
    [SerializeField] private JumpSettings jumpSettings = new JumpSettings();
    [SerializeField] private MouseSettings mouseSettings = new MouseSettings();
    [SerializeField] public UIInfo uiInfo = new UIInfo();
    private Timers timers = new Timers();
    private InternalVars internalVars = new InternalVars();
    private Rigidbody body;
    private Transform cameraTransform;

    private void cameraInput()
    {
        //GPT FPS camera code
        float mouseX = Input.GetAxis("Mouse X") * mouseSettings.mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSettings.mouseSensitivity * Time.fixedDeltaTime;
        internalVars.xRotation -= mouseY;
        internalVars.xRotation = Mathf.Clamp(internalVars.xRotation, -90f, 90f);

        internalVars.yRotation += mouseX;
        //internalVars.yRotation = Mathf.Clamp(0f, internalVars.yRotation, 90f);

        //gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(internalVars.xRotation, 0f, 0f);
        cameraTransform.localRotation = Quaternion.Euler(internalVars.xRotation, internalVars.yRotation, 0f);
        //cameraTransform.Rotate(Vector3.up * mouseX);
    }

    private void releasedJumpAndGravity()
    {
        if (timers.jumpTimer >= jumpSettings.jumpLength || body.velocity.y < 0) internalVars.canJump = false;
        body.AddForce(Vector3.down * movementSettings.gravity * 2);
    }

    private void xyMovementInput() {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = (cameraRight * moveHorizontal + cameraForward * moveVertical).normalized;

        Vector3 targetVelocity;
        if(Input.GetButton("Sprint")) targetVelocity = moveDirection * (movementSettings.maxSpeed * 1.5f);
        else targetVelocity = moveDirection * movementSettings.maxSpeed;
        Vector3 currentVelocity = body.velocity;
        Vector3 newVelocity = Vector3.Lerp(currentVelocity, targetVelocity, movementSettings.accelerationFactor);
        body.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z); // Preserve vertical velocity
        
        if (internalVars.isGrounded) body.velocity = new Vector3(newVelocity.x * movementSettings.groundedTraction, currentVelocity.y, newVelocity.z * movementSettings.groundedTraction);
        else body.velocity = new Vector3(newVelocity.x * movementSettings.aerialTraction, currentVelocity.y, newVelocity.z * movementSettings.aerialTraction);


        //Debug.Log(moveHorizontal + " " + moveVertical);
        //if (moveHorizontal == 0) body.velocity = new Vector3(0, body.velocity.y, body.velocity.z);
        //if (moveVertical == 0) body.velocity = new Vector3(body.velocity.x, body.velocity.y, 0);
    }

    private void jumpInput()
    {
        if ((Input.GetButton("Jump") && internalVars.canJump) || (timers.jumpTimer > 0 && timers.jumpTimer <= (jumpSettings.jumpLength / 2)))
        {
            if(Input.GetButtonDown("Jump")|| internalVars.isGrounded) body.AddForce(Vector3.up * jumpSettings.jumpPower * 5);
            else
            {
                body.AddForce(Vector3.up * jumpSettings.jumpPower);
            }
            timers.jumpTimer++;
            
        }
    }

    private void cancelAngularVelocity()
    {
        body.angularVelocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
        body = GetComponent<Rigidbody>();//grabbing reference to the rigidbody
        Cursor.lockState = CursorLockMode.Locked;//cursor lock
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraInput();
    }

    bool isGrounded()
    {
        // Check if the character is grounded, e.g., using a Raycast
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 2.2f);
    }

    private void FixedUpdate()
    {
        if(isGrounded()) { internalVars.isGrounded = true; internalVars.canJump = true; timers.jumpTimer = 0; }
        else internalVars.isGrounded = false;
        releasedJumpAndGravity();
        xyMovementInput();
        jumpInput();
        cancelAngularVelocity();
    }
}

[System.Serializable]
public class MovementSettings
{
    public float accelerationFactor = 0.35f;
    public float groundedTraction = 0.5f;
    public float aerialTraction = 0.35f;
    public float maxSpeed = 40f;
    //public float walkPower = 20.0f;
    public float gravity = 200f;
}
[System.Serializable]
public class JumpSettings
{
    
    public int jumpLength = 5;
    public float jumpPower = 200.0f;
    
}
[System.Serializable]
public class UIInfo
{
    public int ammo = 10;
    public float health = 100;
}
[System.Serializable]
public class MouseSettings
{
    public float mouseSensitivity = 100f;
}
public class InternalVars
{
    public bool canJump = true;
    public bool isGrounded = true;
    public float xRotation = 0f;
    public float yRotation = 0f;
}

public class Timers
{
    public int jumpTimer = 0;
}
