using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : ItemCtrl
{

    private Transform fireTran;

    public override bool checkUse(ItemCtrl nowItem)
    {//����� ȭ�п� ����Ұ��? �� ���������� ��ó�� Ÿ���Ĺ��� ������ �ڵ����� ����, �ݱ�� ȸ�� 

        return true;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (isGrab==false)
        {
            //�����Q���� �ֺ��� Ÿ���ִ��� üũ�ؼ� ����ֱ� 
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        //���� ���
        //Vector2 dic = rootCtrl.WeaponCtrl.
    }
}
