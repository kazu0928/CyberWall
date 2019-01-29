using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{
    private GameObject gameObject;
    private Animator animator;
    public PlayerAnimator()
    {
        gameObject = PlayerObjectManager.Instance.PlayerModel;
        animator = gameObject.GetComponent<Animator>();
    }
    public void PlayJump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("RunBoad", false);
    }
    public void PlayRunBoad()
    {
        animator.SetBool("RunBoad", true);
    }
    public void StopJump()
    {
        animator.Play("通常モーション");
        animator.SetBool("RunBoad", true);
    }
}
