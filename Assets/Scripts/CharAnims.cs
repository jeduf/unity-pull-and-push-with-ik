using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnims : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if any movement key is pushed.
        bool forward = Input.GetKey("w");
        bool backward = Input.GetKey("s");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        //If one of them is pushed play animation.
        if(forward || backward || left || right)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
