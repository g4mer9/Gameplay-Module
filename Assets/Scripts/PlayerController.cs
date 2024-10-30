using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool can_jump = true;
    private float jump_power = 500.0f;
    private float walk_power = 200.0f;
    private int jump_timer = 0;
    private int jump_length = 10;
    //private int rotate_speed = 5;
    private float xRotation = 0f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float gravity = 120f;
    [SerializeField] private float accelerationFactor = 0.35f;
    [SerializeField] private float maxSpeed = 10f;
    private Rigidbody body;


    private void cameraInput()
    {
        //GPT FPS camera code
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.transform.Rotate(Vector3.up * mouseX);
    }

    private void groundedOrGravity()
    {
        if (body.velocity.y ==0)
        {
            can_jump = true;
            jump_timer = 0;
        }
        else if (jump_timer >= jump_length || (Input.GetButtonUp("Jump") && jump_timer >= (jump_length / 2)) || body.velocity.y < 0) can_jump = false;

        if (!can_jump) body.AddForce(Vector3.down * gravity);
    }

    private void xyMovementInput() {

        if (Input.GetAxis("Horizontal") != 0 && body.velocity.magnitude < maxSpeed)
        {
            
            
            Vector3 targetVelocity = transform.right * walk_power * Input.GetAxis("Horizontal");
            body.velocity = Vector3.Lerp(body.velocity, targetVelocity, Time.deltaTime * accelerationFactor);
            

        }
        if (Input.GetAxis("Vertical") != 0 && body.velocity.magnitude < maxSpeed)
        {
            
            Vector3 targetVelocity = transform.forward * walk_power * Input.GetAxis("Vertical");
            body.velocity = Vector3.Lerp(body.velocity, targetVelocity, Time.deltaTime * accelerationFactor);
            
        }
    }

    private void jumpInput()
    {
        if (Input.GetButton("Jump") && can_jump)
        {
            jump_timer++;
            body.AddForce(Vector3.up * jump_power);
        }
    }

    private void cancelAngularVelocity()
    {
        body.angularVelocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();//grabbing reference to the rigidbody
        Cursor.lockState = CursorLockMode.Locked;//cursor lock
    }

    // Update is called once per frame
    void Update()
    {
        cameraInput();
    }

    private void FixedUpdate()
    {
        groundedOrGravity();
        xyMovementInput();
        jumpInput();
        cancelAngularVelocity();
    }
}
