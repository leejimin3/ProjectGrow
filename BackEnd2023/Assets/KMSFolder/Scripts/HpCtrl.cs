using UnityEngine;

public class HpCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float hp;
    public float maxHp = 30;

    // ü�� ����, �ǰ� ó��, ���¸ӽŰ� ��ȣ�ۿ�
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        rootCtrl.lifeAction += () => { hp = maxHp; rootCtrl.stateCtrl.RemoveStunned(); };
        hp = maxHp;
    }

    public void SetDamaged(float damage, I_Attacker attacker)
    {
        hp -= damage;
        rootCtrl.aggroAction?.Invoke(attacker);
        //rootCtrl.stateCtrl.
        if (hp <= 0)
        {
            attacker.DeadEvent(rootCtrl);
            rootCtrl.deadAction.Invoke();
            //���� �� ȸ�� ����
        }
        else
        {
            rootCtrl.stateCtrl.HitedState();
        }
        // ��׷�
    }
}


