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
    Water = 5,
}

public class Item_Weapon : ItemCtrl
{
    public static Dictionary<WeaponKind, ObjectPooling<BulletCtrl>> bulletPoolDic = new Dictionary<WeaponKind, ObjectPooling<BulletCtrl>>();
    public BulletCtrl bullet;

    public override ItemKind itemKind => ItemKind.Weapon;

    public WeaponKind weaponKind;
    private Transform fireTran;

    private void Start()
    {
        //bullet = ScriptableManager.instance.getTable("Bullet").getPrefab<WeaponInfo>(weaponKind.ToString()).GetComponent<BulletCtrl>();
    }
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
        BulletCtrl newBullet = null;
        if (bulletPoolDic.ContainsKey(weaponKind) == false)
        {
            bulletPoolDic[weaponKind] = new ObjectPooling<BulletCtrl>();
            bulletPoolDic[weaponKind].Initialize(bullet, null, 10);
        }
        newBullet = bulletPoolDic[weaponKind].GetObject(bullet);
        newBullet.dic = dic;
        newBullet.range = 100f;
        newBullet.speed = 1f;
        newBullet.gameObject.SetActive(true);
    }
}
