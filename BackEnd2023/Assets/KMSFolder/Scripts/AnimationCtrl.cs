using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    Animator animator;

    // ���¸ӽſ��� ��û�� �ִϸ��̼� ó��
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        animator = gameObject.GetComponent<Animator>();
    }

    #region �����̻�

    public void IdleAniamtion()
    {
        animator.SetTrigger("Idle");
    }
    public void StunningAnimation()
    {
        animator.SetTrigger("Stunned");
    }

    public void MoveAniamtion()
    {
        animator.SetTrigger("Move");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
    public void GrapAniamtion()
    {
        animator.SetTrigger("Grap");
    }
    public void UseAniamtion()
    {
        animator.SetTrigger("Use");
    }
    public void ThrowAniamtion()
    {
        animator.SetTrigger("Throw");
    }
    #endregion
}
