using System.Collections;
using UnityEngine;
public class EnemyInput : InputCtrl
{
    bool isAttack = false;

    private void Update()
    {
        //transform.position = targetPlayer.position;
        if (rootCtrl.targetTran == null)
        {
            rootCtrl.targetTran = GameManager.instance.ReturnClosesetObject(this.transform);
            rootCtrl.stateCtrl.IdleState();
        }

        if (rootCtrl.targetTran != null && rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
        {
            Vector3 direction = rootCtrl.targetTran.position - transform.position;
            direction.Normalize();

            if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                horizontal = direction.x;
                vertical = direction.y;
            }
            else
            {
                horizontal = 0;
                vertical = 0;
            }
            if (Vector2.Distance(rootCtrl.targetTran.position, transform.position) < 0.3f)
            {
                if (!isAttack)
                {
                    isAttack = true;
                    StartCoroutine(DelayAttack());
                }
            }

        }
    }

    IEnumerator DelayAttack()
    {

        rootCtrl.stateCtrl.AttackState();

        yield return new WaitForSeconds(2f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, LayerManager.Instance.HitZone);

        if (hits != null && hits.Length > 0)
        {
            //�����ؾ���
            foreach (Collider2D hit in hits)
            {
                I_HitZone hitZone = hit.transform.GetComponentInParent<I_HitZone>();
                if (hitZone != null)
                {
                    if (rootCtrl.faction != hitZone.Faction && hitZone.CheckHitLock(rootCtrl) == false)
                    {
                        hitZone.SetDamaged(rootCtrl.scriptableMonster.Atk, rootCtrl);//������ �۾��ؾ���
                        yield return null;
                    }
                }
            }
        }
        rootCtrl.stateCtrl.StateChange(stateEnum.Idle);
        yield return null;
        isAttack = false;

    }

    public override void initiallize()
    {
        base.initiallize();
        //Hp Ctrl aggro reference use
        rootCtrl.aggroAction += (attacker) =>
        {
            rootCtrl.WeaponCtrl.targetTran = attacker.myTransform;
        };

        rootCtrl.deadAction += () =>
        {
            if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                rootCtrl.stateCtrl.stateEnum = stateEnum.Dead;
                rootCtrl.stateCtrl.DeadState();
                StartCoroutine(ExcuteDeadAction());
            }
        };
    }

    IEnumerator ExcuteDeadAction()
    {
        yield return new WaitForSeconds(0.5f);
        rootCtrl.disable();
    }
}
