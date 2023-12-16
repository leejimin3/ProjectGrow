using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyInput : InputCtrl
{
    bool isAttack = false;

    private void Update()
    {
        //transform.position = targetPlayer.position;
        if (rootCtrl.targetTran == null)
        {
            rootCtrl.targetTran = GameManager.instance.ReturnClosesetObject(this.transform);
        }
        
        if (rootCtrl.targetTran != null && rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
        {
            Vector3 direction = rootCtrl.targetTran.position - transform.position;
            direction.Normalize();

            horizontal = direction.x;
            vertical = direction.y;

            StartCoroutine(DelayAttack());
        }
    }

    IEnumerator DelayAttack()
    {

        if (Vector2.Distance(rootCtrl.targetTran.position, transform.position) < 0.3f && isAttack == false)
        {
            isAttack = true;

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
            rootCtrl.stateCtrl.AttackState();
        }
        yield return new WaitForSeconds(2f);

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
    }
}
