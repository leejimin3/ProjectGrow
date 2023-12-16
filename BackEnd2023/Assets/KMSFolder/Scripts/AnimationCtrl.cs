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
        animator.SetTrigger("Stunned");
    }

    public void DeadAnimation()
    {
        animator.SetBool("dead", true);
        animator.SetTrigger("deadCall");
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
