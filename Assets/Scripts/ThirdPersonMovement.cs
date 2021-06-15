using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Public Assignments")]
    public CharacterController controller;
    [Space(10)]

    [Header("Variables")]
    public float speed = 5f;
    public float turnSmoothTime = .1f;
    float turnSmootherSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get WASD Inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Find direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //If direction provided, move the character
        if(direction.magnitude >= 0.1f)
        {
            //Target angle to turn + camera facing direction.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            //Angle to turn character smoothly where its facing.
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootherSpeed, turnSmoothTime);
            //Turn to that angle where character facing.
            transform.rotation = Quaternion.Euler(0f,smoothAngle,0f);
            //Moving direction with camera facing direction.
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Move
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        

    }
}
