using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{
    private GameObject gameObject;
    private Animator animator;
    public PlayerAnimator()
    {
        gameObject = PlayerObjectManager.Instance.PlayerObject;
        animator = gameObject.GetComponent<Animator>();
    }
    public void PlayJump()
    {
        animator.SetTrigger("ジャンプ");
        animator.SetBool("通常モーション", false);
    }
    public void PlayRunBoad()
    {
        animator.SetBool("通常モーション", true);
    }
    public void StopJump()
    {
        animator.Play("通常モーション");
        animator.SetBool("通常モーション", true);
    }
}
