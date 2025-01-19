using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWalking()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isSprinting", false);
        animator.SetBool("isJumping", false);
    }
    public void StopWalking()
    {
        animator.SetBool("isWalking", false);
    }

    public void StartSprinting()
    {
        animator.SetBool("isSprinting", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);
    }
    public void StopSprinting()
    {
        animator.SetBool("isSprinting", false);
    }

    public void StartJumping()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isSprinting", false);
    }
    public void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }
}
