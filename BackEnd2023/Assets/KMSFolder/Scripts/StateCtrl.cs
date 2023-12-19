using UnityEngine;

public enum stateEnum
{
    Idle,
    Stunned,
    Move,
    Dead,
    Grap,
    Use,
    Throw,
    hited,
    Attack
}

public class StateCtrl : MonoBehaviour, InitiallizeInterface
{
    public float time = 0;
    float stunnedTime = 0;
    public stateEnum stateEnum = stateEnum.Idle;
    RootCtrl rootCtrl;


    //���¸ӽ� (���, �ȱ�, �޸���,����,����,����,��ȿ�ۿ�,�̼�) �ִϸ��̼� ��û
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    public void StateChange(stateEnum state)
    {
        switch (state)
        {
            case stateEnum.Idle:
                stateEnum = stateEnum.Idle;
                break;
            case stateEnum.Stunned:
                stateEnum = stateEnum.Stunned;
                break;
            case stateEnum.Move:
                stateEnum = stateEnum.Move;
                break;
            case stateEnum.Dead:
                stateEnum = stateEnum.Dead;
                break;
            case stateEnum.Grap:
                stateEnum = stateEnum.Grap;
                break;
            case stateEnum.Use:
                stateEnum = stateEnum.Use;
                break;
            case stateEnum.Throw:
                stateEnum = stateEnum.Throw;
                break;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time - stunnedTime > 3f && stateEnum == stateEnum.Stunned)
        {
            IdleState();
        }
    }

    public void HitedState()
    {
        //if (IsCanAction(stateEnum))
        //{
        //StateChange(stateEnum.hited);
        rootCtrl.AnimationCtrl.HitedAnimation();
        //stunnedTime = time;
        //}
    }

    #region �����̻� ����
    public void StunState()
    {
        if (stateEnum != stateEnum.Stunned || stateEnum != stateEnum.Dead)
        {
            StateChange(stateEnum.Stunned);
            rootCtrl.AnimationCtrl.StunningAnimation();
            stunnedTime = time;
        }
        //���� ���� (�ð� �߰�)
    }

    public void DeadState()
    {
        if (stateEnum != stateEnum.Dead)
        {
            StateChange(stateEnum.Dead);
            rootCtrl.AnimationCtrl.DeadAnimation();
        }
    }
    public void IdleState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Idle);
            rootCtrl.AnimationCtrl.IdleToMoveAniamtion(0);
        }
    }
    public void WalkState(float pointX, float pointY)
    {
        if (pointY < 0)
        {
            pointY = -pointY;
        }
        Vector2 dic = new Vector2(-pointX, pointY);
        //if (pointX < 0)
        //{
        //    pointX = -pointX;
        //}

        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Move);
            rootCtrl.AnimationCtrl.IdleToMoveAniamtion(dic.magnitude * (dic.x < 0 ? -1 : 1f));
        }
    }
    public void GrapState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Grap);
            rootCtrl.AnimationCtrl.GrapAniamtion();
        }
    }

    public void UseState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Use);
            rootCtrl.AnimationCtrl.UseAniamtion();
        }
    }
    public void ThrowState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Throw);
            rootCtrl.AnimationCtrl.ThrowAniamtion();
        }
    }
    #endregion

    public bool IsCanAction(stateEnum state)
    {
        if (state != stateEnum.Stunned && state != stateEnum.Dead && state != stateEnum.Attack)
        {
            return true;
        }
        return false;
    }


    public void AttackState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Attack;
            rootCtrl.AnimationCtrl.AttackAnimation();
        }
    }
    public void RemoveStunned()
    {
        stateEnum = stateEnum.Idle;
        rootCtrl.AnimationCtrl.RemoveStunningAnimation();
    }
}
