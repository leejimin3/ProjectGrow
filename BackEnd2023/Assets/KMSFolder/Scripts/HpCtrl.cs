using System.Collections;
using System.Collections.Generic;
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
        rootCtrl.lifeAction += () => { hp = maxHp; };
        hp = maxHp;
    }

    public void GetDamaged()
    {
        rootCtrl.stateCtrl.StateChange(stateEnum.Stunned);
        //rootCtrl.stateCtrl.StunState();
        // �÷��̾� ó��
    }

    public void SetDamaged(float damage, RootCtrl attacker)
    {
        hp -=damage;
        if(hp <= 0)
        {
            rootCtrl.deadAction.Invoke();
        }
        // ��׷�
    }
}
