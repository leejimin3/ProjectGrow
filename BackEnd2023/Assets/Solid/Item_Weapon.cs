using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponKind
{
    None = 0,
    Revolver = 1,
    Minigun = 2,
    Firebat = 3,
    Electric = 4,
}

public class Item_Weapon : ItemCtrl
{
    public Transform bullet;

    public WeaponKind weaponKind;
    private Transform fireTran;

    public override bool checkUse(ItemCtrl nowItem)
    {//����� ȭ�п� ����Ұ��? �� ���������� ��ó�� Ÿ���Ĺ��� ������ �ڵ����� ����, �ݱ�� ȸ�� 

        return true;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (isGrab == false)
        {
            //�����Q���� �ֺ��� Ÿ���ִ��� üũ�ؼ� ����ֱ� 
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        //���� ���
        Vector2 dic = (rootCtrl.WeaponCtrl.targetTran.position - fireTran.position);
        float dis = dic.magnitude;
        dic = dic.normalized;
        //źȲ ����, �ش� �������� �߻�
        BulletCtrl newBullet = Instantiate(bullet, null).GetComponent<BulletCtrl>();
        newBullet.dic = dic;
        newBullet.range = 100f;
        newBullet.gameObject.SetActive(true);
    }
}
