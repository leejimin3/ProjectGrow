using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;

    // ü�� ����, �ǰ� ó��, ���¸ӽŰ� ��ȣ�ۿ�
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    public void GetDamaged()
    {
        rootCtrl.stateCtrl.StateChange(stateEnum.Stunned);
        //rootCtrl.stateCtrl.StunState();
        // �÷��̾� ó��
    }
}
