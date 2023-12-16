using UnityEngine;

public class AnimationCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    [SerializeField]Animator animator;

    float IdleMoveValue = 1;
    // ���¸ӽſ��� ��û�� �ִϸ��̼� ó��
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        //animator = gameObject.GetComponent<Animator>();
    }

    #region �����̻�

    public void IdleToMoveAniamtion(float value)
    {
        //animator.SetTrigger("Idle");
        animator.SetFloat("IdleToMove", value);
    }
    public void StunningAnimation()
    {
        animator.SetBool("Stunned",true);
    }

    public void DeadAnimation()
    {
        animator.SetBool("dead", true);
        animator.SetTrigger("deadCall");
    }
    public void GrapAniamtion()
    {
        animator.SetBool("Grap", true);
    }
    public void UseAniamtion()
    {
        animator.SetBool("Use", true);
    }
    public void ThrowAniamtion()
    {
        animator.SetBool("Throw",true);
    }
    public void HitedAnimation()
    {
        animator.SetTrigger("Hited");
    }
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    #endregion
}
